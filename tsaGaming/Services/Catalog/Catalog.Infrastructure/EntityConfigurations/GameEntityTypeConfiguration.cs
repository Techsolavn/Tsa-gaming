using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalog.Infrastructure.EntityConfigurations
{
    class GameEntityTypeConfiguration : IEntityTypeConfiguration<Catalog.Domain.Entites.Game>
    {
        public void Configure(EntityTypeBuilder<Catalog.Domain.Entites.Game> gameConfiguration)
        {
            gameConfiguration.ToTable("Game", CatalogContext.DEFAULT_SCHEMA);

            gameConfiguration.HasKey(o => o.Id);

            gameConfiguration.Property(o => o.Id).UseHiLo("gameseq", CatalogContext.DEFAULT_SCHEMA);

            gameConfiguration.Property(x => x.CreatedAt).IsRequired();

            gameConfiguration.Property(x => x.UpdatedAt).IsRequired();

            gameConfiguration.Property(x => x.Name).IsRequired();

            gameConfiguration.Property(x => x.Description).IsRequired(false);

            gameConfiguration.Property(x => x.LessonId).IsRequired();

            gameConfiguration.Property(x => x.FrameRate).IsRequired();

            gameConfiguration.Property(x => x.IsActive).IsRequired();

            gameConfiguration.Property(x => x.SortIndex).IsRequired();
        }
    }
}
