using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DogsBarberShop.Entities.DomainModels.Interfaces;

namespace DogsBarberShop.Entities.DomainModels
{
    public class Order : BaseEntity, IDateTrackableEntity
    {
        [Required]
        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset? CreationDate { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset? ArrivalDate { get; set; }
        public Guid? PetId { get; set; }
        public string UserId { get; set; }
        public Pet Pet { get; set; }
        public User User { get; set; }
    }
}