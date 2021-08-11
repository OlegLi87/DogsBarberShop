using System.Linq;
using DogsBarberShop_Api.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsBarberShop_Api.Infastructure.Filters
{
    public class ResultFilterAttribute : IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ObjectResult objectResult)
            {
                if (objectResult.Value is AppHttpResponse appRes)
                {
                    context.HttpContext.Response.StatusCode = appRes.StatusCode;
                    if (appRes.IsSuccess)
                        context.Result = new ObjectResult(appRes.Payload.ResponseObject);
                    else
                        context.Result = new ObjectResult(new { Errors = appRes.Payload.ErrorMessages });
                }
                else if (objectResult.Value is ValidationProblemDetails validationProblems)
                {
                    context.HttpContext.Response.StatusCode = 400;
                    context.Result = new ObjectResult(new { Errors = validationProblems.Errors.SelectMany(e => e.Value) });
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        { }
    }
}