using System;
using System.Collections.Generic;

namespace DogsBarberShop.Entities.InfastructureModels
{
    public class BussinessData
    {
        public Dictionary<DayOfWeek, string[]> OpeningHours { get; set; }
        public int AppointmentDurationInMinutes { get; set; }
    }
}