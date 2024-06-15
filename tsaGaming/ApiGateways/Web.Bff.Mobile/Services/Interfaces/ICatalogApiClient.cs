

using Web.Bff.AdminPortal.Models;

namespace Web.Bff.Mobile.Services.Interfaces
{
    public interface ICatalogApiClient
    {
        Task<IEnumerable<CatalogDTO>> GetAllAsync();
    }
}
