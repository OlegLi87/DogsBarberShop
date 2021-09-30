using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.PasswordReset;
using DogsBarberShop.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService<User> _authService;

        public AuthController(IAuthService<User> authService)
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
        public async Task<string> SignIn(SignInCredentials credentials)
        {
            var result = await _authService.SignIn(credentials);
            return result.Payload.ResponseObject;
        }

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<AppResponse<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            return await _authService.ConfirmEmail(token, email);
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<AppResponse<string>> ForgotPassword(ForgotPasswordData forgotPasswordData)
        {
            return await _authService.SendResetPasswordLink(forgotPasswordData);
        }
    }
}