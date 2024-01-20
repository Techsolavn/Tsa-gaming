namespace Catalog.Domain.Interfaces
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IProductRepository : IRepository<Catalog.Domain.Entites.Product>
    {
        Task<Catalog.Domain.Entites.Product> GetAsync(int productId);
        Task<bool> UpdateAsync(Catalog.Domain.Entites.Product product);
    }
}
