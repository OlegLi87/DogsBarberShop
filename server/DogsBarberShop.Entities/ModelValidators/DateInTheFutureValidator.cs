using System;
using System.ComponentModel.DataAnnotations;

namespace DogsBarberShop.Entities.ModelValidators
{
    public class DateInTheFutureValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Date is missing.");

            var arrivalUTCDateTime = (value as DateTimeOffset?).Value.ToUniversalTime();

            if (arrivalUTCDateTime.Subtract(DateTimeOffset.UtcNow) <= TimeSpan.Zero)
                return new ValidationResult("Arrival date must be in the future.");

            return ValidationResult.Success;
        }
    }
}