using System;
using DogsbarberShop.Entities.InfrastructureModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace DogsbarberShop.Controllers.Filters
{
    public class AuthActionFilter : Attribute, IActionFilter
    {
        private readonly IHttpContextAccessor _contextAccesor;

        public AuthActionFilter(IHttpContextAccessor contextAccessor)
        {
            _contextAccesor = contextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null) return;

            var actionName = (_contextAccesor.HttpContext.GetRouteData().Values["action"] as String).ToLower();
            ushort statusCode = 200;

            if (actionName == "signup")
                statusCode = 201;

            object response;

            switch (actionName)
            {
                case "signup":
                    {
                        response = new { Message = "User successfully created.Email confimration link was sent to email address." };
                        break;
                    }
                case "signin":
                    {
                        response = (context.Result as ObjectResult).Value as string;
                        break;
                    }
                case "confirmemail":
                    {
                        response = new { Message = "Email successfully confirmed." };
                        break;
                    }
                case "forgotpassword":
                    {
                        response = new { Message = "Password reset link was sent to email address." };
                        break;
                    }
                case "resetpassword":
                    {
                        response = new { Message = "Password successfully reset." };
                        break;
                    }
                default:
                    {
                        response = new { Message = "Auth operation completed successfully." };
                        break;
                    }
            }

            context.Result = new ObjectResult(new AppResponse
            {
                StatusCode = statusCode,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = response
                }
            });
        }
    }
}