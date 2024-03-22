using Catalog.Domain.Interfaces;

namespace Catalog.API.Application.Queries
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IList<CourseDTO>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<GetCoursesQueryHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public GetCoursesQueryHandler(ICourseRepository courseRepository,
            ILogger<GetCoursesQueryHandler> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IList<CourseDTO>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllAsync();
            _logger.LogInformation("Querying product - Product: {@result}", courses);

            var result = new List<CourseDTO>();
            if (courses == null) return result;
            foreach (var course in courses)
            {
                result.Add(new CourseDTO
                {
                    Id = course.Id,
                    Name = course.Name,
                    DisplayName = course.DisplayName,
                    ImageUrl = course.ImageUrl,
                    IsActive = course.IsActive,
                    IsTop = course.IsTop,
                    SortIndex = course.SortIndex,
                    Lessons = course.Lessons.Select(lesson => 
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
    public record CourseDTO
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
