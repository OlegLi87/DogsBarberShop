using System.Collections.Generic;

namespace DogsbarberShop.Entities.InfrastructureModels
{
    public class AppResponse
    {
        public ushort StatusCode { get; set; }
        public bool IsSuccess => StatusCode < 400;
        public ResponsePayload Payload { get; set; }
        public struct ResponsePayload
        {
            public IEnumerable<string> Errors { get; set; }
            public object ResponseObject { get; set; }
        }
    }
}