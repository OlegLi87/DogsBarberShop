using System.Threading.Tasks;
using DogsBarberShop.Entities.InfastructureModels;

namespace DogsBarberShop.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}