using System.ComponentModel.DataAnnotations;

namespace DogsbarberShop.Entities.Dtos.UserCredentials
{
    public class SignUpCredentials : SignInCredentials
    {
        [Required(ErrorMessage = "Fist name is missing.")]
        [MinLength(2, ErrorMessage = "First name must contain at least 2 characters.")]
        public string FirstName { get; set; }
    }
}