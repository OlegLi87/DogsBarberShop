using System.Threading.Tasks;
using DogsbarberShop.Entities.Dtos.UserCredentials;
using DogsbarberShop.Entities.InfrastructureModels;

namespace DogsBarberShop.Services.AuthService
{
    public interface IAuthService
    {
        Task<AppResponse<string>> SignUp(SignUpCredentials credentials);
        Task<AppResponse<string>> SignIn(SignInCredentials credentials);
    }
}