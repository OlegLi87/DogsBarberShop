using System;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DogsbarberShop.Controllers.Filters
{
    public class UplaodImageSizeLimitResourceFilter : Attribute, IResourceFilter
    {
        private readonly AppSettings _appSettings;

        public UplaodImageSizeLimitResourceFilter(IOptions<AppSettings> opts)
        {
            _appSettings = opts.Value;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            long imageLimitSize = _appSettings.UploadImage.LimitSize;
            if (context.HttpContext.Request.ContentLength > imageLimitSize)
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = 400,
                    Payload = new AppResponse.ResponsePayload
                    {
                        Errors = new[] { $"Maximum allowed sized exceeded.Maximum allowed file size is {imageLimitSize} bytes." }
                    }
                });
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }
}