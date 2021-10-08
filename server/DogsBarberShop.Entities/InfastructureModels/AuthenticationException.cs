using System;
using System.Collections.Generic;

namespace DogsBarberShop.Entities.InfastructureModels
{
    public class AuthenticationException : Exception
    {
        public IEnumerable<string> ExceptionMessages { get; set; }
        public ushort StatusCode { get; set; } = (ushort)400;
    }
}