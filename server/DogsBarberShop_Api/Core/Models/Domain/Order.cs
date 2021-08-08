using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogsBarberShop_Api.Core.Models.Domain
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}