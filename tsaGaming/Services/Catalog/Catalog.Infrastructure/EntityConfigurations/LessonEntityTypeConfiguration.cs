using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Entites.Lesson>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Lesson> lessonConfiguration)
        {
            lessonConfiguration.ToTable("Lesson", CatalogContext.DEFAULT_SCHEMA);

            lessonConfiguration.HasKey(o => o.Id);

            lessonConfiguration.Property(o => o.Id).UseHiLo("lessonseq", CatalogContext.DEFAULT_SCHEMA);

            lessonConfiguration.Property(x => x.CreatedAt).IsRequired();

            lessonConfiguration.Property(x => x.UpdatedAt).IsRequired();

            lessonConfiguration.Property(x => x.Name).IsRequired();

            lessonConfiguration.Property(x => x.Description).IsRequired(false);

            lessonConfiguration.Property(x => x.CatalogId).IsRequired();

            lessonConfiguration.Property(x => x.IsActive).IsRequired();

            lessonConfiguration.Property(x => x.SortIndex).IsRequired();

            lessonConfiguration.HasMany(x => x.Games).WithOne(x => x.Lesson).HasForeignKey(x => x.LessonId).HasPrincipalKey(x => x.Id);
        }
    }
}
