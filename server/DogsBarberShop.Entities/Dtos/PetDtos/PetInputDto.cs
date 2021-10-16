using System.ComponentModel.DataAnnotations;
using DogsBarberShop.Entities.ModelValidators;

namespace DogsBarberShop.Entities.Dtos.PetDtos
{
    public class PetInputDto
    {
        [Required(ErrorMessage = "Pet nickname is missing.")]
        [MaxLength(40, ErrorMessage = "Pet nickname can't contain more than 40 characaters.")]
        [MinLength(2, ErrorMessage = "Pet nickname can't contain less than 2 characters.")]
        public string NickName { get; set; }
        public byte Age { get; set; }

        [FileExistsValidator]
        public string ImagePath { get; set; }
    }
}