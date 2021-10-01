using System.ComponentModel.DataAnnotations;

namespace DogsBarberShop.Entities.Dtos.PasswordReset
{
    public class ForgotPasswordData
    {
        [Required(ErrorMessage = "Email is missing.")]
        [EmailAddress(ErrorMessage = "Email is in wrong format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Reset password url is missing.")]
        [Url(ErrorMessage = "Reset password url is in wrong format.")]
        public string ResetPasswordUrl { get; set; }
    }
}