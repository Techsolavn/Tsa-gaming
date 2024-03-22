namespace Catalog.Domain.Interfaces
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface ICourseRepository : IRepository<Catalog.Domain.Entites.Course>
    {
        Task<IList<Catalog.Domain.Entites.Course>> GetAllAsync();
    }
}
