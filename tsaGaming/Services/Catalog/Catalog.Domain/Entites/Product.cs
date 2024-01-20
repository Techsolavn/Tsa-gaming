using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entites
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public Product()
        {
        }
    }
}
