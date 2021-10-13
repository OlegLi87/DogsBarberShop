using System.ComponentModel.DataAnnotations;

namespace DogsBarberShop.Entities.Dtos.PetDtos
{
    public class PetInputDto
    {
        [Required(ErrorMessage = "Pet nickname is missing.")]
        [MaxLength(40, ErrorMessage = "Pet nickname can't contain more than 40 characaters.")]
        public string NickName { get; set; }
        public byte Age { get; set; }
        public string ImagePath { get; set; }
    }
}