using Catalog.Domain.Interfaces;

namespace Catalog.API.Application.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductQueryHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public GetProductQueryHandler(IProductRepository productRepository,
            ILogger<GetProductQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ProductDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var query = await _productRepository.GetAsync(request.Id);
            _logger.LogInformation("Querying product - Product: {@result}", query);

            var result = new ProductDTO { ProductName = string.Empty };
            if (query != null)
            {
                result.ProductName = query.ProductName;
                result.Id = query.Id;
                result.Description = query.Description;
            }
            return result;
        }
    }
    public record ProductDTO
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
    }
}
