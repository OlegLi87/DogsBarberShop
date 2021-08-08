using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Models;
using DogsBarberShop_Api.Core.Models.Dtos;

namespace DogsBarberShop_Api.Core.Services.AuthService
{
    public interface IAuthService
    {
        Task<AppHttpResponse> SignUp(SignUpCredentialsDto credentials);
        Task<AppHttpResponse> SignIn(SignInCredentialsDto credentials);
    }
}