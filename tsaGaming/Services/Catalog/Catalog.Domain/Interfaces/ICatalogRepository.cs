namespace Catalog.Domain.Interfaces
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface ICatalogRepository : IRepository<Catalog.Domain.Entites.Catalog>
    {
        Task<IList<Catalog.Domain.Entites.Catalog>> GetAllAsync(int page, int itemPerPage);
    }
}
