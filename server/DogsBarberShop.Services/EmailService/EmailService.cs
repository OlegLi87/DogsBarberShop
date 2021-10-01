using System.Threading.Tasks;
using DogsBarberShop.Entities.InfastructureModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DogsBarberShop.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> opts)
        {
            _appSettings = opts.Value;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var message = createMessageToSend(emailMessage);

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_appSettings.Smtp.SmtpServer, _appSettings.Smtp.SmtpPort);
                await client.AuthenticateAsync(_appSettings.Smtp.SmtpUser, _appSettings.Smtp.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private MimeMessage createMessageToSend(EmailMessage emailMessage)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("Dogs Barber Shop", "oleglivcha@gmail.com");
            var to = new MailboxAddress(emailMessage.Address);

            message.From.Add(from);
            message.To.Add(to);
            message.Subject = emailMessage.Subject;

            var bodyBuilder = new BodyBuilder();
            if (emailMessage.BodyType == EmailMessage.EmailMessageBodyType.Html)
                bodyBuilder.HtmlBody = emailMessage.Message;
            else bodyBuilder.TextBody = emailMessage.Message;

            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
    }
}