namespace DogsBarberShop_Api.Core.Models
{
    public class AppHttpResponse
    {
        public ushort StatusCode { get; set; }
        public bool IsSuccess => StatusCode < 400;
        public ResponsePayload<object> Payload { get; set; }

        public struct ResponsePayload<T>
        {
            public T ResponseObject { get; set; }
            public string[] ErrorMessages { get; set; }
        }
    }
}