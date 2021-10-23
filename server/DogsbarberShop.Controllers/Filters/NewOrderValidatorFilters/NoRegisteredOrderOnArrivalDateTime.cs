using System;
using System.Linq;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
using DogsBarberShop.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters.NewOrderValidatorFilters
{
    public class NoRegisteredOrderOnArrivalDateTime : Attribute, IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoRegisteredOrderOnArrivalDateTime(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var orderInputDto = context.ActionArguments["orderInputDto"] as OrderInputDto;
            var preferredDateTime = (DateTimeOffset)context.HttpContext.Items["preferredDateTime"];
            var alternativeDateTime = (DateTimeOffset)context.HttpContext.Items["alternativeDateTime"];

            var orderOnArrivalDateTimes =
              await _unitOfWork.Orders.Get(o => o.ArrivalDate == preferredDateTime || o.ArrivalDate == alternativeDateTime);

            if (orderOnArrivalDateTimes.Count() == 2 ||
                  (orderOnArrivalDateTimes.Count() == 1 && preferredDateTime == alternativeDateTime))
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "There is allready registered order on provided time." }
                    }
                });
                return;
            }

            if (orderOnArrivalDateTimes.Count() == 1)
                orderInputDto.ArrivalDate = orderOnArrivalDateTimes[0].ArrivalDate == preferredDateTime ?
                                     alternativeDateTime : preferredDateTime;

            if (orderOnArrivalDateTimes.Count() == 0)
                orderInputDto.ArrivalDate = preferredDateTime;

            await next();
        }
    }
}