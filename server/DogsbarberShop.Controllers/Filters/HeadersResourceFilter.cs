using System;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class HeadersResourceFilter : Attribute, IResourceFilter
    {
        private string _actionName;
        private IUtilsService _utilsService;

        public HeadersResourceFilter(string actionName, IUtilsService utilsService)
        {
            _actionName = actionName;
            _utilsService = utilsService;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (_actionName == "SignIn")
            {
                var originValue = _utilsService.GetHeaderValue("Origin");
                if (string.IsNullOrEmpty(originValue))
                    context.Result = new ObjectResult(_utilsService.CreateResponseWithErrors<string>(new[] { "Origin header is missing." }));
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }
}