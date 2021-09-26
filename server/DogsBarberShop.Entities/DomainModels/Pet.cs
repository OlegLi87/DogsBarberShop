using System;
using System.ComponentModel.DataAnnotations;
using DogsBarberShop.Entities.DomainModels.Interfaces;

namespace DogsBarberShop.Entities.DomainModels
{
    public class Pet : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string NickName { get; set; }
        public byte Age { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
    }
}