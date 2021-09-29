using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Persistence
{
    public static class SeedDb
    {
        public static void Migrate(DbContext dbContext)
        {
            var dataBase = dbContext.Database;

            if (dataBase.GetPendingMigrations().Any())
                dataBase.Migrate();
        }

        public static void Seed()
        {

        }
    }
}