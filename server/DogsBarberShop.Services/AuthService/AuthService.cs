using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Services.JwtService;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUtilsService _utilsService;
        private readonly IJwtService<User> _jwtService;

        public AuthService(UserManager<User> userManager, IUtilsService utilsService,
                            IJwtService<User> jwtService)
        {
            _userManager = userManager;
            _utilsService = utilsService;
            _jwtService = jwtService;
        }

        public async Task<AppResponse<string>> SignUp(SignUpCredentials credentials)
        {
            var newUser = new User { UserName = credentials.UserName };
            var signUpResult = await _userManager.CreateAsync(newUser, credentials.Password);
            if (!signUpResult.Succeeded)
                return _utilsService.CreateResponseWithErrors<string>(signUpResult.Errors.Select(e => e.Description));

            var firstNameClaim = new Claim(ClaimTypes.Name, credentials.FirstName, ClaimValueTypes.String);
            var addClaimResult = await _userManager.AddClaimAsync(newUser, firstNameClaim);
            if (!addClaimResult.Succeeded)
                return _utilsService.CreateResponseWithErrors<string>(addClaimResult.Errors.Select(e => e.Description));

            var token = _jwtService.CreateToken(newUser, new[] { firstNameClaim });
            return _utilsService.CreateResponseWithPayload<string>(token);
        }

        public async Task<AppResponse<string>> SignIn(SignInCredentials credentials)
        {
            throw new System.NotImplementedException();
        }
    }
}