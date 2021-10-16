using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DogsBarberShop.Entities.ModelValidators
{
    public class FileExistsValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var path = value as string;
            if (path is null) return ValidationResult.Success;

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                return ValidationResult.Success;

            return new ValidationResult("File doesnt exist on provided path.");
        }
    }
}