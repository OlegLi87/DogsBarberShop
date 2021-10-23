using System;
using System.Linq;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters.NewOrderValidatorFilters
{
    public class NoRegisteredOrderForAPetActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoRegisteredOrderForAPetActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var petId = (context.HttpContext.Items["pet"] as Pet).Id;
            var petOrdersList = await _unitOfWork.Orders.Get(o => o.PetId == petId);

            if (petOrdersList.Any())
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { "There is allready registered order with provided pet id." }
                    }
                });
                return;
            }

            await next();
        }
    }
}