namespace DogsBarberShop.Entities.InfastructureModels
{
    public class EmailMessage
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public EmailMessageBodyType BodyType { get; set; } = EmailMessageBodyType.Html;

        public enum EmailMessageBodyType { Html, Text }
    }
}