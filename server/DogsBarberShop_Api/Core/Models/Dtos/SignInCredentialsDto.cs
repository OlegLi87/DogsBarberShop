using System.ComponentModel.DataAnnotations;

namespace DogsBarberShop_Api.Core.Models.Dtos
{
    public class SignInCredentialsDto
    {
        [Required(ErrorMessage = "User name is missing.")]
        [MinLength(3, ErrorMessage = "User name must has at least 3 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is missing.")]
        [MinLength(5, ErrorMessage = "Password must has at least 5 characters.")]
        public string Password { get; set; }
    }
}