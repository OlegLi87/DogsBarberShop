using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;

namespace DogsBarberShop.Services.UtilsService
{
    public interface IUtilsService
    {
        AppResponse<T> CreateResponseWithErrors<T>(IEnumerable<string> errors, ushort statusCode = 400);
        AppResponse<T> CreateResponseWithPayload<T>(T payload, ushort statusCode = 200);
        string GenerateJwtToken(User user, IEnumerable<Claim> claims);
        Task<string> GenerateJwtTokenAsync(User user);
    }
}