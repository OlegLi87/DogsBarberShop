using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.EmailService;
using DogsBarberShop.Services.JwtService;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Services.AuthService
{
    public class AuthService : IAuthService<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUtilsService _utilsService;
        private readonly IJwtService<User> _jwtService;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<User> userManager, IUtilsService utilsService,
                            IJwtService<User> jwtService, IEmailService emailService)
        {
            _userManager = userManager;
            _utilsService = utilsService;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<AppResponse<string>> SignUp(SignUpCredentials credentials)
        {
            var newUser = new User { UserName = credentials.UserName, Email = credentials.Email };
            var signUpResult = await _userManager.CreateAsync(newUser, credentials.Password);
            if (!signUpResult.Succeeded)
                return _utilsService.CreateResponseWithErrors<string>(signUpResult.Errors.Select(e => e.Description));

            var firstNameClaim = new Claim(ClaimTypes.Name, credentials.FirstName, ClaimValueTypes.String);
            var addClaimResult = await _userManager.AddClaimAsync(newUser, firstNameClaim);
            if (!addClaimResult.Succeeded)
                return _utilsService.CreateResponseWithErrors<string>(addClaimResult.Errors.Select(e => e.Description));

            var confirmationLink = await GenerateEmailConfirmationLink(newUser);
            var emailMessage = $"To confirm your email please follow this link <a>{confirmationLink}</a>";
            await _emailService.SendEmailAsync(newUser.Email, emailMessage);

            return _utilsService.CreateResponseWithPayload<string>("Email confirmation message was sent to your email.", 201);
        }

        public async Task<AppResponse<string>> SignIn(SignInCredentials credentials)
        {
            var userInDb = await _userManager.FindByNameAsync(credentials.UserName);
            var isPasswordValid = await _userManager.CheckPasswordAsync(userInDb, credentials.Password);
            if (!isPasswordValid)
                return _utilsService.CreateResponseWithErrors<string>(new[] { "Provided credentials are invalid." });

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(userInDb);
            if (!isEmailConfirmed)
                return _utilsService.CreateResponseWithErrors<string>(new[] { "Email must be confirmed before signing in." });

            var token = await _jwtService.CreateTokenAsync(userInDb);
            return _utilsService.CreateResponseWithPayload<string>(token);
        }

        public async Task<string> GenerateEmailConfirmationLink(User user)
        {
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationData = new EmailConfirmationData
            {
                UserName = user.UserName,
                ConfirmationToken = confirmationToken,
                UrlRedirectTo = _utilsService.GetClientUrl()
            };
            var serializedData = _utilsService.SerializeToBae64<EmailConfirmationData>(confirmationData);

            return $"{_utilsService.GetHostUrl()}/api/auth/confirmEmail/{ serializedData}";
        }

        public async Task<AppResponse<string>> ConfirmEmail(string confirmationData)
        {
            var confirmationDataObj = _utilsService.DeserializeFromBase64<EmailConfirmationData>(confirmationData);

            var userInDb = await _userManager.FindByNameAsync(confirmationDataObj.UserName);
            var confirmationResult = await _userManager.ConfirmEmailAsync(userInDb, confirmationDataObj.ConfirmationToken);
            if (!confirmationResult.Succeeded)
                return _utilsService.CreateResponseWithErrors<string>(confirmationResult.Errors.Select(e => e.Description));

            return _utilsService.CreateResponseWithPayload<string>(confirmationDataObj.UrlRedirectTo);
        }
    }
}