using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagement.Infrastructure;
using Polly;

namespace OrderManagement.Infrastructure
{
    internal class OrderManagementDbInitializer
    {
        private readonly OrderManagementContext _context;
        private readonly ILogger<OrderManagementDbInitializer> _logger;

        public OrderManagementDbInitializer(OrderManagementContext context, ILogger<OrderManagementDbInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

       // public void MigrateDatabase()
       // {
       //     _logger.LogInformation("Migrating database associated with DevOpsContext");
       //
       //     try
       //     {
       //         IEnumerable<string> pendingMigrations = _context.Database.GetPendingMigrations();
       //         //if the sql server container is not created on run docker compose this migration can't fail for network related exception.
       //         var retry = Policy.Handle<SqlException>()
       //             .WaitAndRetry(new TimeSpan[]
       //             {
       //                 TimeSpan.FromSeconds(3),
       //                 TimeSpan.FromSeconds(5),
       //                 TimeSpan.FromSeconds(8),
       //             });
       //         retry.Execute(() => _context.Database.Migrate());
       //
       //         _logger.LogInformation("Migrated database associated with DevOpsContext");
       //     }
       //     catch (Exception ex)
       //     {
       //         _logger.LogError(ex, "An error occurred while migrating the database used on DevOpsContext");
       //     }
       // }

      // public void SeedData()
      // {
      //     //Probleem opgelost door manuele migratie
      //     if (_context.Teams.Any())
      //     {
      //         //Seeding already happened
      //         return;
      //     }
      //
      //     _context.Teams.AddRange(new[]
      //     {
      //         Team.CreateNew("Alpha"),
      //         Team.CreateNew("Beta")
      //     });
      //
      //     _context.Developers.AddRange(new[]
      //     {
      //         Developer.CreateNew("20210920001", "Kris", "Hermans", 0.5),
      //         Developer.CreateNew("20210920002", "Wesley", "Hendrikx", 0.5)
      //     });
      //
      //     _context.SaveChanges();
      // }
    }
}