using System;
using System.Threading.Tasks;
using DogsBarberShop.Entities.Repositories;

namespace DogsBarberShop.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IOrdersRepository Orders { get; }
        IPetsRepository Pets { get; }
        Task SaveAsync();
    }
}