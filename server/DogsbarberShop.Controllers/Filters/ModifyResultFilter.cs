using System;
using DogsbarberShop.Entities.InfrastructureModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsbarberShop.Controllers.Filters
{
    public class ModifyResultFilter : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {

        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }
}