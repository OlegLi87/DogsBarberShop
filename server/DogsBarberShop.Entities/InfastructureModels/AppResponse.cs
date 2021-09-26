using System.Collections.Generic;

namespace DogsbarberShop.Entities.InfrastructureModels
{
    public class AppResponse<T>
    {
        public ushort StatusCode { get; set; }
        public bool IsSuccess => StatusCode < 400;
        public ResponsePayload<T> Payload { get; set; }
        public struct ResponsePayload<U>
        {
            public IEnumerable<string> Errors { get; set; }
            public U ResponseObject { get; set; }
        }
    }
}