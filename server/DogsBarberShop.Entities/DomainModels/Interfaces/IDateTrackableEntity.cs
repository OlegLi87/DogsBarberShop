using System;

namespace DogsBarberShop.Entities.DomainModels.Interfaces
{
    public interface IDateTrackableEntity
    {
        DateTime? CreationDate { get; set; }
    }
}