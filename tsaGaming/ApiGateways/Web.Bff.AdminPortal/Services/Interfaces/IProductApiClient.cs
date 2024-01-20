using Web.Bff.AdminPortal.Models;

namespace Web.Bff.AdminPortal.Services.Interfaces
{
    public interface IProductApiClient
    {
        Task<ProductItem> GetItemByIdAsync(int id);

        Task UpdateAsync(ProductItem item);
    }
}
