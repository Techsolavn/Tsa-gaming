using Catalog.API.Application.Commands;
using Catalog.Domain.Interfaces;

namespace Catalog.API.Application.Validations
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(ILogger<CreateProductCommandValidator> logger)
        {
            RuleFor(p => p.Id).GreaterThanOrEqualTo(0).WithMessage("product Id must be greater than 0");
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("No product name found");

            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
