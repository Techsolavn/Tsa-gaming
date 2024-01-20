using Catalog.Domain.Interfaces;

namespace Catalog.API.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateProductCommandHandler(IProductRepository productRepository,
            ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var query = await _productRepository.GetAsync(request.Id);
            _logger.LogInformation("Querying product - Product: {@result}", query);


            query.ProductName = request.ProductName;
            query.Description = request.Description;

            return await _productRepository.UpdateAsync(query);
        }
    }
}
