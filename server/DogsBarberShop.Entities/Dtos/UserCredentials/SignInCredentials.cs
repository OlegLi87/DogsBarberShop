using System.ComponentModel.DataAnnotations;

namespace DogsbarberShop.Entities.Dtos.UserCredentials
{
    public class SignInCredentials
    {
        [Required(ErrorMessage = "User name is missing.")]
        [MinLength(3, ErrorMessage = "User name must contain at least 3 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is missing.")]
        [MinLength(5, ErrorMessage = "Password must contain at least 5 characters.")]
        public string Password { get; set; }
    }
}