using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Dtos.PasswordReset;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Services.EmailService;
using DogsBarberShop.Services.JwtService;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace DogsBarberShop.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUtilsService _utilsService;
        private readonly IJwtService<User> _jwtService;
        private readonly IEmailService _emailService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IUtilsService utilsService,
                            IJwtService<User> jwtService, IEmailService emailService,
                             IOptions<AppSettings> opts, IMapper mapper)
        {
            _userManager = userManager;
            _utilsService = utilsService;
            _jwtService = jwtService;
            _emailService = emailService;
            _appSettings = opts.Value;
            _mapper = mapper;
        }

        public async Task SignUp(SignUpCredentials credentials)
        {
            var newUser = new User { UserName = credentials.UserName, Email = credentials.Email };
            var signUpResult = await _userManager.CreateAsync(newUser, credentials.Password);
            if (!signUpResult.Succeeded)
                throw _mapper.Map<AuthenticationException>(signUpResult);

            var firstNameClaim = new Claim(ClaimTypes.Name, credentials.FirstName, ClaimValueTypes.String);
            var addClaimResult = await _userManager.AddClaimAsync(newUser, firstNameClaim);
            if (!addClaimResult.Succeeded)
                throw _mapper.Map<AuthenticationException>(addClaimResult);

            await SendEmailConfirmationLink(newUser.Email, credentials.EmailConfirmationUrl);
        }

        public async Task<string> SignIn(SignInCredentials credentials)
        {
            var userInDb = await _userManager.FindByNameAsync(credentials.UserName);
            var isPasswordValid = await _userManager.CheckPasswordAsync(userInDb, credentials.Password);
            if (!isPasswordValid)
                throw new AuthenticationException { ExceptionMessages = new[] { "Provided credentials are invalid." } };

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(userInDb);
            if (!isEmailConfirmed)
                throw new AuthenticationException { ExceptionMessages = new[] { "Email is not confirmed." }, StatusCode = 401 };

            return await _jwtService.CreateTokenAsync(userInDb);
        }

        public async Task SendEmailConfirmationLink(string email, string emailConfirmationUrl)
        {
            var userInDb = await _userManager.FindByEmailAsync(email);
            if (userInDb is null)
                throw new AuthenticationException { ExceptionMessages = new[] { "Provided credentials are invalid." } };

            var link = await generateEmailConfirmationLink(userInDb, emailConfirmationUrl);
            var message = $"To confirm your email please follow this link <a>{link}</a>";
            var emailMessage = new EmailMessage
            {
                Address = email,
                Subject = "Email confirmation link.",
                Message = message
            };
            await _emailService.SendEmailAsync(emailMessage);
        }

        public async Task ConfirmEmail(string token, string email)
        {
            var userInDb = await _userManager.FindByEmailAsync(email);
            var confirmationResult = await _userManager.ConfirmEmailAsync(userInDb, token);
            if (!confirmationResult.Succeeded)
                throw _mapper.Map<AuthenticationException>(confirmationResult);
        }

        public async Task SendResetPasswordLink(ForgotPasswordData forgotPasswordData)
        {
            var userInDb = await _userManager.FindByEmailAsync(forgotPasswordData.Email);
            if (userInDb is null)
                throw new AuthenticationException { ExceptionMessages = new[] { "Provided credentials are invalid." } };

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(userInDb);
            if (!isEmailConfirmed)
                throw new AuthenticationException { ExceptionMessages = new[] { "Email is not confirmed." }, StatusCode = 401 };

            var link = await generateResetPasswordLink(userInDb, forgotPasswordData.ResetPasswordUrl);
            var message = $"In order to reset your password please follow this link <a>{link}</a>";
            var emailMessage = new EmailMessage
            {
                Address = userInDb.Email,
                Subject = "Reset password link.",
                Message = message,
            };
            await _emailService.SendEmailAsync(emailMessage);
        }

        public async Task ResetPassword(string newPassword, string token, string email)
        {
            var userInDb = await _userManager.FindByEmailAsync(email);
            var resetResult = await _userManager.ResetPasswordAsync(userInDb, token, newPassword);
            if (!resetResult.Succeeded)
                throw _mapper.Map<AuthenticationException>(resetResult);
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

            return QueryHelpers.AddQueryString(emailConfirmationUrl, queryParams);
        }

        private async Task<string> generateResetPasswordLink(User user, string resetPasswordUrl)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var queryParams = new Dictionary<string, string>
            {
                ["token"] = resetToken,
                ["email"] = user.Email
            };

            return QueryHelpers.AddQueryString(resetPasswordUrl, queryParams);
        }
    }
}