namespace DogsBarberShop_Api.Core.Models
{
    public class AppHttpResponse
    {
        public ushort StatusCode { get; set; }
        public bool IsSuccess => StatusCode < 400;
        public ResponsePayload Payload { get; set; }

        public struct ResponsePayload
        {
            public object ResponseObject { get; set; }
            public string[] ErrorMessages { get; set; }
        }
    }
}