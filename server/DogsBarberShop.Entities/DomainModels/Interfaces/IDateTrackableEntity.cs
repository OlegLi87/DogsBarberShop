using System;

namespace DogsBarberShop.Entities.DomainModels.Interfaces
{
    public interface IDateTrackableEntity
    {
        DateTimeOffset? CreationDate { get; set; }
    }
}