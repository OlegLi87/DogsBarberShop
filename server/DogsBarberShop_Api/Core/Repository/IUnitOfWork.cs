namespace DogsBarberShop_Api.Core.Repository
{
    public interface IUnitOfWork
    {
        IOrdersRepository Orders { get; set; }
    }
}