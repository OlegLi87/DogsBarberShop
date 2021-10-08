using System;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class AuthExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AuthenticationException authException)
            {
                context.ExceptionHandled = true;
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = authException.StatusCode,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = authException.ExceptionMessages
                    }
                });
            }
        }
    }
}