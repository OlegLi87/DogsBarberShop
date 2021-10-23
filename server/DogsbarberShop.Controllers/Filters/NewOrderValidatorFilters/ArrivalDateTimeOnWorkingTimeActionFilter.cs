using System;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.UnitOfWork;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DogsbarberShop.Controllers.Filters.NewOrderValidatorFilters
{
    public class ArrivalDateTimeOnWorkingTimeActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilsService _utilsService;
        private readonly BussinessData _bussinesData;

        public ArrivalDateTimeOnWorkingTimeActionFilter(IUnitOfWork unitOfWork, IUtilsService utilsService,
                                                          IOptions<BussinessData> opts)
        {
            _unitOfWork = unitOfWork;
            _utilsService = utilsService;
            _bussinesData = opts.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var orderInputDto = context.ActionArguments["orderInputDto"] as OrderInputDto;
            var localArrivalDateTime = orderInputDto.ArrivalDate.Value.ToLocalTime();

            bool isArrivalOnWorkingTime = true;

            try
            {
                var workingHours = _bussinesData.OpeningHours[localArrivalDateTime.DayOfWeek];
                var opensDateTime = _utilsService.CreateParsedDate(localArrivalDateTime, workingHours[0], "HH:mm");
                var closesDateTime = _utilsService.CreateParsedDate(localArrivalDateTime, workingHours[1], "HH:mm");

                if (opensDateTime > localArrivalDateTime || closesDateTime <= localArrivalDateTime)
                    throw new ArgumentException();

                context.HttpContext.Items["opensDateTime"] = opensDateTime;
                context.HttpContext.Items["closesDateTime"] = closesDateTime;
                context.HttpContext.Items["localArrivalDateTime"] = localArrivalDateTime;
            }
            catch (Exception)
            {
                isArrivalOnWorkingTime = false;
            }

            if (!isArrivalOnWorkingTime)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "Business is closed." }
                    }
                });

                return;
            }

            await next();
        }
    }
}