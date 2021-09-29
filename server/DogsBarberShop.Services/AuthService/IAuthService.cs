using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Services.AuthService
{
    public interface IAuthService<T> where T : IdentityUser
    {
        Task<AppResponse<string>> SignUp(SignUpCredentials credentials);
        Task<AppResponse<string>> SignIn(SignInCredentials credentials);
        Task<string> GenerateEmailConfirmationLink(T user);
        Task<AppResponse<string>> ConfirmEmail(string confirmationData);
    }
}