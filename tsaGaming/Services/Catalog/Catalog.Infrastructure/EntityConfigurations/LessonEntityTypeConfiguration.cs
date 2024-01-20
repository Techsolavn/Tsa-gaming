using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Entites.Lesson>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Lesson> courseConfiguration)
        {
            courseConfiguration.ToTable("Lesson", CatalogContext.DEFAULT_SCHEMA);

            courseConfiguration.HasKey(o => o.Id);

            courseConfiguration.Property(o => o.Id).UseHiLo("lessonseq", CatalogContext.DEFAULT_SCHEMA);

            courseConfiguration.Property(x => x.CreatedAt).IsRequired();

            courseConfiguration.Property(x => x.UpdatedAt).IsRequired();

            courseConfiguration.Property(x => x.Name).IsRequired();

            courseConfiguration.Property(x => x.Description).IsRequired(false);

            courseConfiguration.Property(x => x.CourseId).IsRequired();

            courseConfiguration.Property(x => x.IsActive).IsRequired();

            courseConfiguration.Property(x => x.SortIndex).IsRequired();

            courseConfiguration.HasMany(x => x.Games).WithOne(x => x.Lesson).HasForeignKey(x => x.LessonId).HasPrincipalKey(x => x.Id);
        }
    }
}
