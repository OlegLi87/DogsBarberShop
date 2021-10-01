using System.ComponentModel.DataAnnotations;

namespace DogsBarberShop.Entities.Dtos.PasswordReset
{
    public class ResetPasswordData
    {
        [Required(ErrorMessage = "Password is missing.")]
        [MinLength(5, ErrorMessage = "Password must contain at least 5 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is missing.")]
        [Compare(nameof(Password), ErrorMessage = "Provided passwords dont match.")]
        public string ConfirmPassword { get; set; }
    }
}