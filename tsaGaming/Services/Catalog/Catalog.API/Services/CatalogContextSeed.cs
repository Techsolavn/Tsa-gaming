using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Catalog.Infrastructure;

namespace Catalog.API.Services
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CatalogContextSeed));

            var prodList = new List<Catalog.Domain.Entites.Product>
            {
                new Domain.Entites.Product{ProductName="a",Description="a1",CreatedAt=DateTimeOffset.Now },
                new Domain.Entites.Product{ProductName="b",Description="c1",CreatedAt=DateTimeOffset.Now },
                new Domain.Entites.Product{ProductName="c",Description="b1",CreatedAt=DateTimeOffset.Now }
            };
            
            await policy.ExecuteAsync(async () =>
            {

                //var useCustomizationData = settings.Value.UseCustomizationData;

                //var contentRootPath = env.ContentRootPath;
                using (context)
                {
                    await context.Database.MigrateAsync();
                    bool isAny = await context.Products.AnyAsync();
                    if (!isAny)
                    {
                        context.Products.AddRange(prodList);
                        await context.SaveChangesAsync();
                    }
                }
            });
        }
        private AsyncRetryPolicy CreatePolicy(ILogger<CatalogContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Error seeding database (attempt {retry} of {retries})", prefix, retry, retries);
                    }
                );
        }
    }

}
