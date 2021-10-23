using System;
using System.Linq;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.OrderDtos;
using DogsBarberShop.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class ProvideEntityActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private Type _entityType;
        public ProvideEntityActionFilter(IUnitOfWork unitOfWork, Type entityType)
        {
            _unitOfWork = unitOfWork;
            _entityType = entityType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerName = context.RouteData.Values["controller"].ToString().ToLower();
            var userId = context.HttpContext.User.Claims.First(c => c.Type == "id").Value.ToString();

            if (_entityType == typeof(Pet))
            {
                Guid petId;
                if (controllerName == "pets")
                    petId = new Guid(context.RouteData.Values["petId"] as string);
                else
                    petId = (context.ActionArguments["orderInputDto"] as OrderInputDto).PetId.Value;

                var petsList = await _unitOfWork.Pets.Get(p => p.Id == petId && p.UserId == userId);

                if (!petsList.Any())
                {
                    context.Result = new ObjectResult(new AppResponse
                    {
                        StatusCode = 404,
                        Payload = new AppResponse.ResponsePayload
                        {
                            Errors = new[] { "User doesn't have a pet with provided id." }
                        }
                    });
                    return;
                }
                context.HttpContext.Items["pet"] = petsList[0];
            }
            else if (_entityType == typeof(Order))
            {
                var orderId = new Guid(context.RouteData.Values["orderId"] as string);

                var ordersList = await _unitOfWork.Orders.Get(o => o.Id == orderId && o.UserId == userId);
                if (!ordersList.Any())
                {
                    context.Result = new ObjectResult(new AppResponse
                    {
                        StatusCode = 404,
                        Payload = new AppResponse.ResponsePayload
                        {
                            Errors = new[] { "User doesn't have an order with provided id." }
                        }
                    });
                    return;
                }
                context.HttpContext.Items["order"] = ordersList[0];
            }

            await next();
        }
    }
}