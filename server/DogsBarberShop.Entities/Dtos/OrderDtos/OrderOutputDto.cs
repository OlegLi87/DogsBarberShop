using System;

namespace DogsBarberShop.Entities.Dtos.OrderDtos
{
    public class OrderOutputDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public Guid? PetId { get; set; }
        public string PetNickname { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}