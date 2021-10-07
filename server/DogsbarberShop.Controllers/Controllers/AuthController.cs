using System.Threading.Tasks;
using DogsbarberShop.Controllers.Filters;
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
        public async Task<AppResponse<string>> SignUp(SignUpCredentials credentials)
        {
            return await _authService.SignUp(credentials);
        }

        [HttpPost]
        [Route("signin")]
        // Users can only signin with Origin header in request.
        [TypeFilter(typeof(HeadersResourceFilter), Arguments = new[] { nameof(SignIn) })]
        public async Task<AppResponse<string>> SignIn(SignInCredentials credentials)
        {
            return await _authService.SignIn(credentials);
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

        [HttpPost]
        [Route("resetPassword")]
        public async Task<AppResponse<string>> ResetPassword(ResetPasswordData passwordData, [FromQuery] string token,
                                                              [FromQuery] string email)
        {
            return await _authService.ResetPassword(passwordData.Password, token, email);
        }
    }
}