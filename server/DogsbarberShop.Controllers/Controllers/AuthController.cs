using DogsbarberShop.Entities.Dtos.UserCredentials;
using Microsoft.AspNetCore.Mvc;

namespace DogsbarberShop.Controllers.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public sealed class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(SignUpCredentials credentials)
        {
            return Ok();
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult SignIn(SignInCredentials credentials)
        {
            return NotFound();
        }
    }
}