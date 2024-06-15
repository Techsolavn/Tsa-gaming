using Catalog.Domain.Interfaces;

namespace Catalog.API.Application.Queries
{
    public class GetCatalogsQueryHandler : IRequestHandler<GetCatalogsQuery, IList<CatalogDTO>>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly ILogger<GetCatalogsQueryHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public GetCatalogsQueryHandler(ICatalogRepository catalogRepository,
            ILogger<GetCatalogsQueryHandler> logger)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IList<CatalogDTO>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
        {
            var catalogs = await _catalogRepository.GetAllAsync(request.Page, request.ItemPerPage);
            _logger.LogInformation("Querying product - Product: {@result}", catalogs);

            var result = new List<CatalogDTO>();
            if (catalogs == null) return result;
            foreach (var catalog in catalogs)
            {
                result.Add(new CatalogDTO
                {
                    Id = catalog.Id,
                    Name = catalog.Name,
                    DisplayName = catalog.DisplayName,
                    ImageUrl = catalog.ImageUrl,
                    IsActive = catalog.IsActive,
                    IsTop = catalog.IsTop,
                    SortIndex = catalog.SortIndex,
                    Lessons = catalog.Lessons.Select(lesson => 
                        new LessonDTO {
                            Name = lesson.Name,
                            IsActive= lesson.IsActive,
                            SortIndex= lesson.SortIndex,
                            Games = lesson.Games.Select(game => 
                                new GameDTO 
                                {
                                    Name = game.Name,
                                    IsActive = game.IsActive,
                                    SortIndex = game.SortIndex,
                                }).ToList(),
                        }).ToList(),
                });
            }
            return result;
        }
    }
    public record CatalogDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string DisplayName { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsTop { get; set; }
        public int SortIndex { get; set; }
        public required IList<LessonDTO> Lessons { get; set; }
    }

    public record LessonDTO
    {
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
        public required IList<GameDTO> Games { get; set; }
    }

    public record GameDTO
    {
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
    }
}
