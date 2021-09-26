using System;

namespace DogsBarberShop.Entities.DomainModels.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}