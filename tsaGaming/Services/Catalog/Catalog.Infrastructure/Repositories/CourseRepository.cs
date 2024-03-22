using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Entites;

namespace Catalog.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CourseRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(course => course.Lessons)
                .ThenInclude(lesson => lesson.Games)
                .ToListAsync();
        }
    }
}
