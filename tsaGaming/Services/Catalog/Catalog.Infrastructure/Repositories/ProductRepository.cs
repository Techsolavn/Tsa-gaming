using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Interfaces;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Catalog.Domain.Entites.Product> GetAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(o => o.Id == productId);
            return product;
        }

        public async Task<bool> UpdateAsync(Domain.Entites.Product product)
        {
            _context.Update(product);

            return await UnitOfWork.SaveEntitiesAsync();
        }
    }
}
