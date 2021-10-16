using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class ProvideUserIdActionFilter : Attribute, IActionFilter
    {
        public string ItemValueName { get; set; }
        public string ClaimTypeName { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var id = claims.First(c => c.Type == ClaimTypeName).Value;
            context.HttpContext.Items[ItemValueName] = id;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}