using System.Collections.Generic;
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
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace DogsBarberShop.Services.AuthService
{
    public class AuthService : IAuthService<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUtilsService _utilsService;
        private readonly IJwtService<User> _jwtService;
        private readonly IEmailService _emailService;
        private readonly AppSettings _appSettings;

        public AuthService(UserManager<User> userManager, IUtilsService utilsService,
                            IJwtService<User> jwtService, IEmailService emailService, IOptions<AppSettings> opts)
        {
            _userManager = userManager;
            _utilsService = utilsService;
            _jwtService = jwtService;
            _emailService = emailService;
            _appSettings = opts.Value;
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

            await SendEmailConfirmationLink(newUser, credentials.EmailConfirmationUrl);
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
                return _utilsService.CreateResponseWithErrors<string>(new[] { "Email must be confirmed before signing in." }, 401);

            var token = await _jwtService.CreateTokenAsync(userInDb);
            return _utilsService.CreateResponseWithPayload<string>(token);
        }

        public async Task SendEmailConfirmationLink(User user, string emailConfirmationUrl)
        {
            var link = await generateEmailConfirmationLink(user, emailConfirmationUrl);
            var emailMessage = $"To confirm your email please follow this link <a>{link}</a>";
            await _emailService.SendEmailAsync(user.Email, emailMessage);
        }

        public async Task<AppResponse<string>> ConfirmEmail(string token, string email)
        {
            var userInDb = await _userManager.FindByEmailAsync(email);
            var confirmationResult = await _userManager.ConfirmEmailAsync(userInDb, token);
            if (!confirmationResult.Succeeded)
                return _utilsService.CreateResponseWithErrors<string>(confirmationResult.Errors.Select(e => e.Description));

            return _utilsService.CreateResponseWithPayload<string>("Email successfully confirmed!");
        }

        private async Task<string> generateEmailConfirmationLink(User user, string emailConfirmationUrl)
        {
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var queryParams = new Dictionary<string, string>
            {
                ["token"] = confirmationToken,
                ["email"] = user.Email
            };

            if (string.IsNullOrEmpty(emailConfirmationUrl))
                emailConfirmationUrl = $"{_utilsService.GetHostUrl()}/{_appSettings.ConfirmEmailPath}";

            var link = QueryHelpers.AddQueryString(emailConfirmationUrl, queryParams);
            return link;
        }
    }
}