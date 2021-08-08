using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Models;
using DogsBarberShop_Api.Core.Models.Domain;
using DogsBarberShop_Api.Core.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop_Api.Core.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<AppHttpResponse> SignUp(SignUpCredentialsDto credentials)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppHttpResponse> SignIn(SignInCredentialsDto credentials)
        {
            throw new System.NotImplementedException();
        }
    }
}