using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogsBarberShop.Entities.DomainModels
{
    public class Pet : BaseEntity
    {
        [Required]
        [MaxLength(40)]
        public string NickName { get; set; }
        public byte Age { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
    }
}