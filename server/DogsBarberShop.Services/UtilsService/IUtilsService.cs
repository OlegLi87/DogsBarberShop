using System.Collections.Generic;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using Microsoft.AspNetCore.Http;

namespace DogsBarberShop.Services.UtilsService
{
    public interface IUtilsService
    {
        AppResponse CreateResponseWithErrors(IEnumerable<string> errors, ushort statusCode = 400);
        AppResponse CreateResponseWithPayload(object payload, ushort statusCode = 200);
        string GetHostUrl();
        string GetHeaderValue(string header);
        string SerializeToBae64<T>(T obj);
        T DeserializeFromBase64<T>(string base64String);
        Task SaveFileAsync(string path, IFormFile file);
        Dictionary<string, dynamic> MapPropertiesToDictionary(object obj);
    }
}