using DogsBarberShop.Entities.ModelValidators;
using Microsoft.AspNetCore.Http;

namespace DogsBarberShop.Entities.Dtos
{
    public class UploadImageDto
    {
        [UploadImageValidator]
        public IFormFile Image { get; set; }
    }
}