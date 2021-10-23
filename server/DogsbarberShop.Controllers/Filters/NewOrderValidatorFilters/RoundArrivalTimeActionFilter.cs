using System;
using DogsBarberShop.Entities.InfastructureModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace DogsbarberShop.Controllers.Filters.NewOrderValidatorFilters
{
    // Rounds arrival time to the closest appointment according to appointment duration.
    // Creates preffered dateTime (closest one to appointment) and alternative (farest one from appointment)
    public class RoundArrivalTimeActionFilter : Attribute, IActionFilter
    {
        private readonly BussinessData _bussinesData;

        public RoundArrivalTimeActionFilter(IOptions<BussinessData> opts)
        {
            _bussinesData = opts.Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var localArrivalDateTime = (DateTimeOffset)context.HttpContext.Items["localArrivalDateTime"];
            var opensDateTime = (DateTimeOffset)context.HttpContext.Items["opensDateTime"];
            var closesDateTime = (DateTimeOffset)context.HttpContext.Items["closesDateTime"];
            int appointmentDuration = _bussinesData.AppointmentDurationInMinutes;

            var preferredDateTime = localArrivalDateTime;
            var alternativeDateTime = preferredDateTime;

            if (closesDateTime.Subtract(localArrivalDateTime).Minutes < appointmentDuration)
            {
                preferredDateTime = closesDateTime.AddMinutes(-appointmentDuration);
                alternativeDateTime = preferredDateTime;
            }
            else
            {
                int minutesFromOpening = localArrivalDateTime.Subtract(opensDateTime).Hours * 60
                                                      + localArrivalDateTime.Subtract(opensDateTime).Minutes;
                int mod = minutesFromOpening % appointmentDuration;

                if (mod != 0 && mod < appointmentDuration / 2)
                {
                    preferredDateTime = localArrivalDateTime.AddMinutes(-mod);
                    alternativeDateTime = localArrivalDateTime.AddMinutes(appointmentDuration - mod);
                }
                else if (mod != 0 && mod > appointmentDuration / 2)
                {
                    preferredDateTime = localArrivalDateTime.AddMinutes(appointmentDuration - mod);
                    alternativeDateTime = localArrivalDateTime.AddMinutes(-mod);
                }
            }

            context.HttpContext.Items["preferredDateTime"] = preferredDateTime;
            context.HttpContext.Items["alternativeDateTime"] = alternativeDateTime;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}