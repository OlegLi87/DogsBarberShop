using System;
using System.Linq;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class ProvideEntityActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private string _routeValueName;
        private string _claimTypeName;
        private string _itemValueName;

        public ProvideEntityActionFilter(IUnitOfWork unitOfWork, string routeValueName,
                                          string claimTypeName, string itemValueName)
        {
            _unitOfWork = unitOfWork;
            _routeValueName = routeValueName;
            _claimTypeName = claimTypeName;
            _itemValueName = itemValueName;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerName = context.RouteData.Values["controller"].ToString().ToLower();
            var targetId = new Guid(context.RouteData.Values[_routeValueName].ToString());
            var userId = context.HttpContext.User.Claims.First(c => c.Type == _claimTypeName).Value.ToString();

            if (controllerName == "pets")
            {
                var pets = await _unitOfWork.Pets.Get(p => p.Id == targetId && p.UserId == userId);
                if (!pets.Any())
                {
                    context.Result = new ObjectResult(new AppResponse
                    {
                        StatusCode = 400,
                        Payload = new AppResponse.ResponsePayload
                        {
                            Errors = new[] { "User doesn't have a pet with provided id." }
                        }
                    });
                    return;
                }

                context.HttpContext.Items[_itemValueName] = pets[0];
            }

            if (controllerName == "orders") { }

            await next();
        }
    }
}