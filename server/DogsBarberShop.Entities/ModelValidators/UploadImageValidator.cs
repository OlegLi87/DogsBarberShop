using System.ComponentModel.DataAnnotations;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DogsBarberShop.Entities.ModelValidators
{
    public class UploadImageValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is IFormFile file)
            {
                var appSettings = context.GetService<IOptions<AppSettings>>().Value;
                var delimeterIndex = file.ContentType.LastIndexOf('/');
                if (delimeterIndex > -1)
                {
                    var fileType = file.ContentType.Substring(delimeterIndex + 1);
                    foreach (var format in appSettings.UploadImage.AllowedFormats)
                        if (format == fileType)
                            return ValidationResult.Success;
                }

                return new ValidationResult("Provided file format is not supported.");
            }
            else
                return new ValidationResult("No file located within a form data.");
        }
    }
}