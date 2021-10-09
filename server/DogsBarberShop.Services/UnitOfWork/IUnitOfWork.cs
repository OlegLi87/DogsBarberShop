using System;
using DogsBarberShop.Entities.Repositories;

namespace DogsBarberShop.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOrdersRepository Orders { get; }
        IPetsRepository Pets { get; }
    }
}