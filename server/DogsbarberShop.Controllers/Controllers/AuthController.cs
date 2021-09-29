using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<string> SignUp(SignUpCredentials credentials)
        {
            var res = await _authService.SignUp(credentials);
            return res.Payload.ResponseObject;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult SignIn(SignInCredentials credentials)
        {
            return NotFound();
        }
    }
}