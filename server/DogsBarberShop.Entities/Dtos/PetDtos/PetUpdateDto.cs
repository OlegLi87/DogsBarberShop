using System.Runtime;
using DogsBarberShop.Entities.ModelValidators;

namespace DogsBarberShop.Entities.Dtos.PetDtos
{
    public class PetUpdateDto
    {
        public string NickName { get; set; }
        public byte? Age { get; set; }

        [FileExistsValidator]
        public string ImagePath { get; set; }
    }
}