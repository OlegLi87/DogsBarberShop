using System;

namespace DogsBarberShop.Entities.Models.Interfaces
{
    public interface IDateTrackableEntity
    {
        DateTime? CreationDate { get; set; }
    }
}