using System.ComponentModel.DataAnnotations;

namespace DogsBarberShop_Api.Core.Models.Dtos
{
    public class SignUpCredentialsDto : SignInCredentialsDto
    {
        [Required(ErrorMessage = "First name is missing.")]
        [MinLength(2, ErrorMessage = "First name must has at least 2 characters.")]
        [RegularExpression("[a-zA-Z]", ErrorMessage = "First name can has only letters")]
        public string FirstName { get; set; }
    }
}