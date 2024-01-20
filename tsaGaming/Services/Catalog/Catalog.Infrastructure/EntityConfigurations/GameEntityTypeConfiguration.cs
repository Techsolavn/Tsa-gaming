using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class GameEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Entites.Game>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Game> courseConfiguration)
        {
            courseConfiguration.ToTable("Game", CatalogContext.DEFAULT_SCHEMA);

            courseConfiguration.HasKey(o => o.Id);

            courseConfiguration.Property(o => o.Id).UseHiLo("gameseq", CatalogContext.DEFAULT_SCHEMA);

            courseConfiguration.Property(x => x.CreatedAt).IsRequired();

            courseConfiguration.Property(x => x.UpdatedAt).IsRequired();

            courseConfiguration.Property(x => x.Name).IsRequired();

            courseConfiguration.Property(x => x.Description).IsRequired(false);

            courseConfiguration.Property(x => x.LessonId).IsRequired();

            courseConfiguration.Property(x => x.FrameRate).IsRequired();

            courseConfiguration.Property(x => x.IsActive).IsRequired();

            courseConfiguration.Property(x => x.SortIndex).IsRequired();
        }
    }
}
