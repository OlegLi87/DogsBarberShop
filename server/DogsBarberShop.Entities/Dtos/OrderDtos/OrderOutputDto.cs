using System;

namespace DogsBarberShop.Entities.Dtos.OrderDtos
{
    public class OrderOutputDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public Guid PetId { get; set; }
        public string PetName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}