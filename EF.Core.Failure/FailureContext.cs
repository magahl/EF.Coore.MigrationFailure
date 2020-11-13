using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace EF.Core.Failure
{
    public class FailureContext : DbContext
    {
        public FailureContext(ILogger<FailureContext> logger)
        {

        }

        public FailureContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var currentDirectory = Directory.GetCurrentDirectory();
            var appsettingsJson = Path.Combine(currentDirectory, "appsettings.json");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory);
            configurationBuilder.AddJsonFile(appsettingsJson);
            IConfiguration configuration = configurationBuilder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FailureDb"));
            optionsBuilder.UseLazyLoadingProxies();
        }

        DbSet<Item> Items { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
    }

}