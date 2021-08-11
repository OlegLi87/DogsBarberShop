using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop_Api.Persistence
{
    public static class InitDb
    {
        public static void Migrate(DbContext dbContext)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate();
        }
    }
}