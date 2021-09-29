using System.Threading.Tasks;

namespace DogsBarberShop.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string address, string messageBody);
    }
}