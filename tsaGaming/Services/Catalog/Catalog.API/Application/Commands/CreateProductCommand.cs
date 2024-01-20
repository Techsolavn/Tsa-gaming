using System.Text.Json.Serialization;

namespace Catalog.API.Application.Commands
{
    public class CreateProductCommand : IRequest<bool>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public CreateProductCommand() { }

    }
}
