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

        [Column(TypeName = "smalldatetime")]
        public DateTime OrderTime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ArrivalTime { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}