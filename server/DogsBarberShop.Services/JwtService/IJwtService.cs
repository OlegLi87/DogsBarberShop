using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Services.JwtService
{
    public interface IJwtService<T> where T : IdentityUser
    {
        string CreateToken(T user, IEnumerable<Claim> claims);
        Task<string> CreateTokenAsync(T user);
    }
}