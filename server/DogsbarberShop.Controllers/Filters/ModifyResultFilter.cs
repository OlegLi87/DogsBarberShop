using System;
using System.Linq;
using DogsbarberShop.Entities.InfrastructureModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class ModifyResultFilter : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objResult)
            {
                if (objResult.Value is AppResponse appResponseResult)
                {
                    context.HttpContext.Response.StatusCode = appResponseResult.StatusCode;
                    if (appResponseResult.IsSuccess)
                        context.Result = new ObjectResult(appResponseResult.Payload.ResponseObject);
                    else
                        context.Result = new ObjectResult(new { errors = appResponseResult.Payload.Errors });
                }
                else if (objResult.Value is ValidationProblemDetails valProblemsResult)
                {
                    context.HttpContext.Response.StatusCode = 400;
                    context.Result = new ObjectResult(new { errors = valProblemsResult.Errors.Values.SelectMany(v => v) });
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }
}