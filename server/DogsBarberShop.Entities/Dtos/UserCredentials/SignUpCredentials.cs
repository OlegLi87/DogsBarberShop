using System.ComponentModel.DataAnnotations;

namespace DogsbarberShop.Entities.Dtos.UserCredentials
{
    public class SignUpCredentials : SignInCredentials
    {
        [Required(ErrorMessage = "First name is missing.")]
        [MinLength(2, ErrorMessage = "First name must contain at least 2 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Password confirmation is missing.")]
        [Compare(nameof(Password), ErrorMessage = "Provided passwords dont match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is missing.")]
        [EmailAddress(ErrorMessage = "Provided email is in wrong format.")]
        public string Email { get; set; }
    }
}