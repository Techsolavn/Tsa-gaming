using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Entites;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CatalogRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<Domain.Entites.Catalog>> GetAllAsync(int page, int itemPerPage)
        {
            return await _context.Catalogs
                .Include(catalog => catalog.Lessons)
                .ThenInclude(lesson => lesson.Games)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToListAsync();
        }
    }
}
