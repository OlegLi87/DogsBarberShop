using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class ProvideUserIdActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var id = claims.First(c => c.Type == "id").Value;
            context.HttpContext.Items["userId"] = id;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}