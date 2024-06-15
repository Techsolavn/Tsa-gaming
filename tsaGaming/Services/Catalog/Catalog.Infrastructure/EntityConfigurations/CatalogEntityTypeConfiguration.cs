using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class CatalogEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Entites.Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Catalog> catalogConfiguration)
        {
            catalogConfiguration.ToTable("Catalog", CatalogContext.DEFAULT_SCHEMA);

            catalogConfiguration.HasKey(o => o.Id);

            catalogConfiguration.Property(o => o.Id).UseHiLo("catalogseq", CatalogContext.DEFAULT_SCHEMA);

            catalogConfiguration.Property(x => x.CreatedAt).IsRequired();

            catalogConfiguration.Property(x => x.UpdatedAt).IsRequired();

            catalogConfiguration.Property(x => x.Name).IsRequired();

            catalogConfiguration.Property(x => x.Description).IsRequired(false);

            catalogConfiguration.Property(x => x.Price).IsRequired();

            catalogConfiguration.Property(x => x.PriceVAT).IsRequired();

            catalogConfiguration.Property(x => x.IsTop).IsRequired();

            catalogConfiguration.Property(x => x.IsActive).IsRequired();

            catalogConfiguration.Property(x => x.SortIndex).IsRequired();

            catalogConfiguration.HasMany(x => x.Lessons).WithOne(x => x.Catalog).HasForeignKey(x => x.CatalogId).HasPrincipalKey(x => x.Id);
        }
    }
}
