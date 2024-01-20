using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entites
{
    public class Product : Entity, IAggregateRoot
    {
        public DateTimeOffset CreatedAt { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }

        public Product()
        {
        }
        

    }
}
