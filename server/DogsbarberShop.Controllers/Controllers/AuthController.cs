using System.Threading.Tasks;
using DogsbarberShop.Controllers.Filters;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsBarberShop.Entities.Dtos.PasswordReset;
using DogsBarberShop.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [ServiceFilter(typeof(AuthActionFilter))]
    [AuthExceptionFilter]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task SignUp(SignUpCredentials credentials)
        {
            await _authService.SignUp(credentials);
        }

        [HttpPost]
        [Route("signin")]
        // Users can only signin with Origin header in request.
        [TypeFilter(typeof(HeadersResourceFilter), Arguments = new[] { nameof(SignIn) })]
        public async Task<string> SignIn(SignInCredentials credentials)
        {
            return await _authService.SignIn(credentials);
        }

        [HttpGet]
        [Route("confirmEmail")]
        public async Task ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            await _authService.ConfirmEmail(token, email);
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task ForgotPassword(ForgotPasswordData forgotPasswordData)
        {
            await _authService.SendResetPasswordLink(forgotPasswordData);
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task ResetPassword(ResetPasswordData passwordData, [FromQuery] string token,
                                                              [FromQuery] string email)
        {
            await _authService.ResetPassword(passwordData.Password, token, email);
        }
    }
}