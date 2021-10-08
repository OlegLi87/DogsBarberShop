using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsBarberShop.Entities.Dtos.PasswordReset;

namespace DogsBarberShop.Services.AuthService
{
    public interface IAuthService
    {
        Task SignUp(SignUpCredentials credentials);
        Task<string> SignIn(SignInCredentials credentials);
        Task SendEmailConfirmationLink(string email, string emailConfirmationUrl);
        Task ConfirmEmail(string token, string email);
        Task SendResetPasswordLink(ForgotPasswordData forgotPasswordData);
        Task ResetPassword(string newPassword, string token, string email);
    }
}