using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Models;
using DogsBarberShop_Api.Core.Models.Dtos;
using DogsBarberShop_Api.Core.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace DogsBarberShop_Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost]
        [Route("signup")]
        public Task<AppHttpResponse> SignUp([FromBody] SignUpCredentialsDto credentials)
        {
            return null;
        }

        [HttpPost]
        [Route("signin")]
        public Task<AppHttpResponse> SignIn([FromBody] SignInCredentialsDto credentials)
        {
            return null;
        }
    }
}