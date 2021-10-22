using System;
using System.ComponentModel.DataAnnotations;
using DogsBarberShop.Entities.ModelValidators;

namespace DogsBarberShop.Entities.Dtos.OrderDtos
{
    public class OrderInputDto
    {
        [Required(ErrorMessage = "Pet id is missing.")]
        public Guid? PetId { get; set; }

        [DateInTheFutureValidator]
        public DateTimeOffset? ArrivalDate { get; set; }
    }
}