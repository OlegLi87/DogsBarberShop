using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DogsBarberShop.Entities.Models.Interfaces;

namespace DogsBarberShop.Entities.Models
{
    public class Order : IEntity, IDateTrackableEntity
    {
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreationDate { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime? ArrivalDate { get; set; }

        [Required]
        public Guid? PetId { get; set; }
        public Pet Pet { get; set; }
    }
}