using System.Collections.Generic;
using DogsbarberShop.Entities.InfrastructureModels;

namespace DogsBarberShop.Services.UtilsService
{
    public interface IUtilsService
    {
        AppResponse<T> CreateResponseWithErrors<T>(IEnumerable<string> errors, ushort statusCode = 400);
        AppResponse<T> CreateResponseWithPayload<T>(T payload, ushort statusCode = 200);
        string GetHostUrl();
        string GetClientUrl();
    }
}