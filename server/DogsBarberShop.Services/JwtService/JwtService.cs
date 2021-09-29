using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DogsBarberShop.Services.JwtService
{
    public class JwtService : IJwtService<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUtilsService _utilsService;

        public JwtService(UserManager<User> userManager, IOptions<AppSettings> opts, IUtilsService utilsService)
        {
            _userManager = userManager;
            _appSettings = opts.Value;
            _utilsService = utilsService;
        }

        public string CreateToken(User user, IEnumerable<Claim> claims)
        {
            var secretBytes = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var firstName = claims.First(c => c.Type == ClaimTypes.Name).Value;
            var allClaims = new List<Claim>();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(getAllClaims(user, claims)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha512),
                Audience = _utilsService.GetClientUrl(),
                Issuer = _utilsService.GetHostUrl(),
                Expires = DateTime.Now.AddDays(10),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return CreateToken(user, claims);
        }

        private IEnumerable<Claim> getAllClaims(User user, IEnumerable<Claim> claims)
        {
            var firstName = claims.First(c => c.Type == ClaimTypes.Name).Value;
            var allClaims = new List<Claim>();

            allClaims.Add(new Claim("id", user.Id));
            allClaims.Add(new Claim("userName", user.UserName));
            allClaims.Add(new Claim("firstName", firstName));

            return allClaims;
        }

    }
}