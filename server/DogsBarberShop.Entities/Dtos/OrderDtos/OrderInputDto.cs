using System;
using DogsBarberShop.Entities.ModelValidators;

namespace DogsBarberShop.Entities.Dtos.OrderDtos
{
    public class OrderInputDto
    {
        [OrderInputPetIdValidator]
        public Guid? PetId { get; set; }

        [DateInTheFutureValidator]
        public DateTimeOffset? ArrivalDate { get; set; }
    }
}