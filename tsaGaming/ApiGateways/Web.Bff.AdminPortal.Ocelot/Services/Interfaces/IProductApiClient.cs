using Web.Bff.AdminPortal.Ocelot.Models;

namespace Web.Bff.AdminPortal.Ocelot.Services.Interfaces
{
    public interface IProductApiClient
    {
        Task<ProductItem> GetItemByIdAsync(int id);

        Task UpdateAsync(ProductItem item);
    }
}
