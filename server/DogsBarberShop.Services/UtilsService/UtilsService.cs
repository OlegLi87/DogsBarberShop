using System.Collections.Generic;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DogsBarberShop.Services.UtilsService
{
    public class UtilsService : IUtilsService
    {
        private readonly AppSettings _appSetings;
        private readonly IHttpContextAccessor _contextAccessor;

        public UtilsService(IOptions<AppSettings> opts, IHttpContextAccessor contextAccessor)
        {
            _appSetings = opts.Value;
            _contextAccessor = contextAccessor;
        }

        public AppResponse<T> CreateResponseWithErrors<T>(IEnumerable<string> errors, ushort statusCode = 400)
        {
            return new AppResponse<T>
            {
                StatusCode = statusCode,
                Payload = new AppResponse<T>.ResponsePayload<T>
                {
                    Errors = errors
                }
            };
        }

        public AppResponse<T> CreateResponseWithPayload<T>(T payload, ushort statusCode = 200)
        {
            return new AppResponse<T>
            {
                StatusCode = statusCode,
                Payload = new AppResponse<T>.ResponsePayload<T>
                {
                    ResponseObject = payload
                }
            };
        }

        public string GetHostUrl()
        {
            var request = _contextAccessor.HttpContext.Request;
            return $"{request.Scheme}//{request.Host}";
        }

        public string GetClientUrl()
        {
            var request = _contextAccessor.HttpContext.Request;
            var clientUrl = request.Headers["Origin"];
            return clientUrl == default(StringValues) ? string.Empty : clientUrl;
        }
    }
}