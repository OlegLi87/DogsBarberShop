using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUtilsService _utilsService;

        public AuthService(UserManager<User> userManager, IUtilsService utilsService)
        {
            _userManager = userManager;
            _utilsService = utilsService;
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
                return _utilsService.CreateResponseWithErrors<string>(signUpResult.Errors.Select(e => e.Description));


        }

        public async Task<AppResponse<string>> SignIn(SignInCredentials credentials)
        {
            throw new System.NotImplementedException();
        }
    }
}