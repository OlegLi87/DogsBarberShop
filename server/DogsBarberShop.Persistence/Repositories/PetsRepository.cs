using System.Linq;
using System.Threading.Tasks;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Persistence.Repositories
{
    public class PetsRepository : GenericRepository<Pet>, IPetsRepository
    {
        public PetsRepository(DbContext context) : base(context)
        {
        }

        // Before deleting pet model, it's correspondent order model PetId 
        // value must be set to null manually since delete behavior is NoAction.
        public async override Task Delete(params Pet[] pets)
        {
            var orders = Context.Set<Order>();
            var petIds = pets.Select(p => p.Id);
            var petOrders = await orders.Where(o => petIds.Any(id => id.Equals(o.Id)))
                                        .ToListAsync();

            foreach (var order in petOrders)
                order.PetId = null;

            await base.Delete(pets);
        }
    }
}