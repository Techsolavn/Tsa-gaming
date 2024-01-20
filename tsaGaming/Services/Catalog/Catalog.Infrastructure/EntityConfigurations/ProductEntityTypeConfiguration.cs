using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class ProductEntityTypeConfiguration: IEntityTypeConfiguration<Catalog.Domain.Entites.Product>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Product> productConfiguration)
        {
            productConfiguration.ToTable("Product", CatalogContext.DEFAULT_SCHEMA);

            productConfiguration.HasKey(o => o.Id);

            productConfiguration.Property(o => o.Id).UseHiLo("productseq", CatalogContext.DEFAULT_SCHEMA);

            productConfiguration
                .Property<DateTimeOffset>("CreatedAt")
                .HasColumnName("CreatedAt")
                .IsRequired();

            productConfiguration
                .Property<string>("ProductName")
                .HasColumnName("Name")
                .IsRequired();

            productConfiguration.Property<string>("Description").IsRequired(false);
        }
    }
}
