using System;
using DogsbarberShop.Entities.InfrastructureModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class PetsOrdersExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var controllerName = context.RouteData.Values["controller"] as string;
            var actionName = context.RouteData.Values["action"] as string;

            (string, string) phrases = getPhrases(actionName.ToLower(), controllerName.ToLower());

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(new AppResponse
            {
                StatusCode = 500,
                Payload = new AppResponse.ResponsePayload
                {
                    Errors = new[] { $"Internal error occured while {phrases.Item2} {phrases.Item1}(s)." }
                }
            });
        }

        private (string, string) getPhrases(string actionName, string controllerName)
        {
            string entityName, actionOnEntityName;

            if (controllerName.IndexOf("pets") != -1)
                entityName = "pet";
            else
                entityName = "order";

            if (actionName.IndexOf("add") != -1)
                actionOnEntityName = "adding";
            else if (actionName.IndexOf("get") != -1)
                actionOnEntityName = "getting";
            else if (actionName.IndexOf("delete") != -1)
                actionOnEntityName = "deleting";
            else if (actionName.IndexOf("update") != -1)
                actionOnEntityName = "updating";
            else
                actionOnEntityName = "working on";

            return (entityName, actionOnEntityName);
        }
    }
}