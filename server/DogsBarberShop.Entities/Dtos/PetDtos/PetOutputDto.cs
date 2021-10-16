using System;

namespace DogsBarberShop.Entities.Dtos.PetDtos
{
    public class PetOutputDto : PetInputDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}