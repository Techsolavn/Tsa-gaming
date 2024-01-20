namespace Catalog.API.Application.Queries
{
    public class GetProductQuery : IRequest<ProductDTO>
    {
        public int Id { get; set; }
        public GetProductQuery() { }

    }
}
