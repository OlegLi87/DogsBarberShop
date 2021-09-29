using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
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
        [Route("confirmEmail/{confirmData}")]
        public async Task<AppResponse<string>> ConfirmEmail([FromRoute] string confirmData)
        {
            return await _authService.ConfirmEmail(confirmData);
        }
    }
}