using System;
using System.Linq;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.UnitOfWork;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DogsbarberShop.Controllers.Filters
{
    // Validates:
    // 1) User has a pet.
    // 2) There is no allready registered order with provided pet id.
    // 3) Arrival dateTime falls on working time.
    // 4) There is no allready registered order on arrival datetime.
    public class NewOrderValidatorActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilsService _utilsService;
        private readonly BussinessData _bussinessData;
        private readonly string _actionArgumentName;

        public NewOrderValidatorActionFilter(IUnitOfWork unitOfWork, IUtilsService utilsService,
                                              string actionArgumentName, IOptions<BussinessData> opts)
        {
            _unitOfWork = unitOfWork;
            _utilsService = utilsService;
            _actionArgumentName = actionArgumentName;
            _bussinessData = opts.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var orderInputDto = context.ActionArguments[_actionArgumentName] as OrderInputDto;
            var arrivalLocalDateTime = orderInputDto.ArrivalDate.Value.ToLocalTime();
            var petId = orderInputDto.PetId;
            var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value;

            // validate user has a pet
            var pets = await _unitOfWork.Pets.Get(p => p.Id == petId && p.UserId == userId);
            if (pets.Count == 0)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "User doesn't has a pet with provided id." }
                    }
                });
                return;
            }

            // validate there is no orders with provided pet
            var orderWithPet = await _unitOfWork.Orders.Get(o => o.PetId == petId);
            if (orderWithPet.Any())
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "There is allready registered order for provided pet id." }
                    }
                });
                return;
            }

            context.HttpContext.Items["userId"] = userId;

            var opensDate = new DateTimeOffset();
            var closesDate = new DateTimeOffset();
            bool isArrivalOnOpeningHours = true;

            // validating that arrival time falls on opening hours in working day.
            try
            {
                var openingHours = _bussinessData.OpeningHours[arrivalLocalDateTime.DayOfWeek];
                opensDate = _utilsService.CreateParsedDate(arrivalLocalDateTime, openingHours[0], "HH:mm");
                closesDate = _utilsService.CreateParsedDate(arrivalLocalDateTime, openingHours[1], "HH:mm");

                if (opensDate > arrivalLocalDateTime || arrivalLocalDateTime > closesDate)
                    isArrivalOnOpeningHours = false;
            }
            catch (Exception)
            {
                isArrivalOnOpeningHours = false;
            }

            if (!isArrivalOnOpeningHours)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "Bussiness is closed." }
                    }
                });
                return;
            }

            // rounding arrival time to nearest appointment time.
            (int, int) minutesToRound = roundArrivalDateToNearestAppointment(arrivalLocalDateTime, opensDate, closesDate, _bussinessData.AppointmentDurationInMinutes);
            var preferredDateTime = orderInputDto.ArrivalDate.Value.AddMinutes(minutesToRound.Item1);
            var secondaryDateTime = orderInputDto.ArrivalDate.Value.AddMinutes(minutesToRound.Item2);

            // validating there are no allready registered orders on arrival datetime.
            var orders = await _unitOfWork.Orders.Get(o => o.ArrivalDate == preferredDateTime || o.ArrivalDate == secondaryDateTime);

            if (orders.Count == 2 || orders.Count == 1 && preferredDateTime == secondaryDateTime)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "Arrival datetime allready taken." }
                    }
                });
                return;
            }
            if (orders.Count == 1)
            {
                var roundedArrivalDate = orders[0].ArrivalDate == preferredDateTime ? secondaryDateTime : preferredDateTime;
                orderInputDto.ArrivalDate = roundedArrivalDate;
                await next();
            }
            else if (orders.Count == 0)
            {
                orderInputDto.ArrivalDate = preferredDateTime;
                await next();
            }
        }

        // Item1 of returned tuple respresents preferred value to around (closest to appointment),second is the a far one.
        private (int, int) roundArrivalDateToNearestAppointment(DateTimeOffset arrivalDate, DateTimeOffset opensDate,
                                                                  DateTimeOffset closesDate, int appointmentDuration)
        {
            var spanFromOpening = arrivalDate.Subtract(opensDate);
            int minutesFromOpening = spanFromOpening.Hours * 60 + spanFromOpening.Minutes;
            int mod = minutesFromOpening % appointmentDuration;

            if (closesDate == arrivalDate)
                return (-appointmentDuration, -appointmentDuration);

            if (closesDate.Subtract(arrivalDate).Minutes < appointmentDuration)
                return (-mod, -mod);

            if (mod == 0) return (0, 0);
            if (mod <= appointmentDuration / 2) return (-mod, appointmentDuration - mod);
            return (appointmentDuration - mod, -mod);
        }
    }
}