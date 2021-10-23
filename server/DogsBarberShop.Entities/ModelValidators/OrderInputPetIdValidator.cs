using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DogsBarberShop.Entities.ModelValidators
{
    public class OrderInputPetIdValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContext = validationContext.GetService<IHttpContextAccessor>().HttpContext;
            bool isOnUpdate = httpContext.GetRouteData().Values["action"]
                                                        .ToString().ToLower().Contains("update");

            if (!isOnUpdate && value is null)
                return new ValidationResult("Pet id is missing.");

            return ValidationResult.Success;
        }
    }
}