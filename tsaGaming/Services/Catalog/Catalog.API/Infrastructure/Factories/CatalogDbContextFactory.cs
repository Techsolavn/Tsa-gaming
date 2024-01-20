using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Catalog.Infrastructure;

namespace Catalog.API.Infrastructure.Factories
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Catalog.API"));

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
