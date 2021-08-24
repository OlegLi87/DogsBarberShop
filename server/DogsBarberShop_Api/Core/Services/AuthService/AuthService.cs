using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Models;
using DogsBarberShop_Api.Core.Models.Domain;
using DogsBarberShop_Api.Core.Models.Dtos;
using DogsBarberShop_Api.Infastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DogsBarberShop_Api.Core.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<AppSettings> _opts;
        public AuthService(UserManager<AppUser> userManager, IOptions<AppSettings> opts)
        {
            _userManager = userManager;
            _opts = opts;
        }

        public async Task<AppHttpResponse> SignUp(SignUpCredentialsDto credentials)
        {
            var newUser = new AppUser { UserName = credentials.UserName };
            var signUpResult = await _userManager.CreateAsync(newUser, credentials.Password);
            if (!signUpResult.Succeeded)
                return createResponseWithErrors(signUpResult.Errors.Select(e => e.Description));

            var firstNameClaim = new Claim(ClaimTypes.Name, credentials.FirstName, ClaimValueTypes.String);
            var addClaimResult = await _userManager.AddClaimAsync(newUser, firstNameClaim);
            if (!addClaimResult.Succeeded)
                return createResponseWithErrors(addClaimResult.Errors.Select(e => e.Description));

            var jwt = await createJwt(newUser);
            return createResponseWithJwt(jwt, 201);
        }

        public async Task<AppHttpResponse> SignIn(SignInCredentialsDto credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.UserName);
            var signInSuccess = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!signInSuccess)
                return createResponseWithErrors(new string[] { "Provided credentials are invalid." });

            var jwt = await createJwt(user);
            return createResponseWithJwt(jwt, 200);
        }

        private async Task<string> createJwt(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var jwtSecret = (_opts.Value as AppSettings).JwtSecret;
            var secretBytes = Encoding.ASCII.GetBytes(jwtSecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userName",user.UserName,ClaimValueTypes.String),
                    new Claim("firstName",claims.First(c => c.Type == ClaimTypes.Name).Value,ClaimValueTypes.String)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha512),
                Expires = DateTime.Now.AddDays(15)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private AppHttpResponse createResponseWithJwt(string jwt, ushort statusCode = 200)
        {
            return new AppHttpResponse
            {
                StatusCode = statusCode,
                Payload = new AppHttpResponse.ResponsePayload
                {
                    ResponseObject = jwt
                }
            };
        }

        private AppHttpResponse createResponseWithErrors(IEnumerable<string> errors, ushort statusCode = 400)
        {
            return new AppHttpResponse
            {
                StatusCode = statusCode,
                Payload = new AppHttpResponse.ResponsePayload
                {
                    ErrorMessages = errors.ToArray()
                }
            };
        }
    }
}