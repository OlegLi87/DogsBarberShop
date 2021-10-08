using System;
using System.Collections.Generic;
using System.Text;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

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

        public AppResponse CreateResponseWithErrors(IEnumerable<string> errors, ushort statusCode = 400)
        {
            return new AppResponse
            {
                StatusCode = statusCode,
                Payload = new AppResponse.ResponsePayload
                {
                    Errors = errors
                }
            };
        }

        public AppResponse CreateResponseWithPayload(object payload, ushort statusCode = 200)
        {
            return new AppResponse
            {
                StatusCode = statusCode,
                Payload = new AppResponse.ResponsePayload
                {
                    ResponseObject = payload
                }
            };
        }

        public string GetHostUrl()
        {
            var request = _contextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        public string GetHeaderValue(string header)
        {
            var request = _contextAccessor.HttpContext.Request;
            var headerValue = request.Headers[header];
            return headerValue == default(StringValues) ? string.Empty : headerValue;
        }

        public string SerializeToBae64<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var jsonBytes = Encoding.ASCII.GetBytes(json);
            return Convert.ToBase64String(jsonBytes);
        }

        public T DeserializeFromBase64<T>(string base64String)
        {
            var jsonBytes = Convert.FromBase64String(base64String);
            var json = Encoding.ASCII.GetString(jsonBytes);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}