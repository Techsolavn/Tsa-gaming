using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Entites.Course>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Course> courseConfiguration)
        {
            courseConfiguration.ToTable("Course", CatalogContext.DEFAULT_SCHEMA);

            courseConfiguration.HasKey(o => o.Id);

            courseConfiguration.Property(o => o.Id).UseHiLo("courseseq", CatalogContext.DEFAULT_SCHEMA);

            courseConfiguration.Property(x => x.CreatedAt).IsRequired();

            courseConfiguration.Property(x => x.UpdatedAt).IsRequired();

            courseConfiguration.Property(x => x.Name).IsRequired();

            courseConfiguration.Property(x => x.Description).IsRequired(false);

            courseConfiguration.Property(x => x.Price).IsRequired();

            courseConfiguration.Property(x => x.PriceVAT).IsRequired();

            courseConfiguration.Property(x => x.IsTop).IsRequired();

            courseConfiguration.Property(x => x.IsActive).IsRequired();

            courseConfiguration.Property(x => x.SortIndex).IsRequired();

            courseConfiguration.HasMany(x => x.Lessons).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).HasPrincipalKey(x => x.Id);
        }
    }
}
