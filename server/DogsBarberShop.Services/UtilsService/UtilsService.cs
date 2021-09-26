using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DogsBarberShop.Services.UtilsService
{
    public class UtilsService : IUtilsService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSetings;

        public UtilsService(UserManager<User> userManager, IOptions<AppSettings> opts)
        {
            _userManager = userManager;
            _appSetings = opts.Value;
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

        public async string GenerateJwtToken(User user, IEnumerable<Claim> claims)
        {
            var jwtSecret = _appSetings.JwtSecret;
            var secretBytes = Encoding.ASCII.GetBytes(jwtSecret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                  new Claim("Id",user.Id,ClaimValueTypes.String),
               }),
            };
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {

        }
    }
}