using Microsoft.Extensions.Options;
using System.Text.Json;
using Web.Bff.AdminPortal.Ocelot.Models;
using Web.Bff.AdminPortal.Ocelot.Services.Interfaces;

namespace Web.Bff.AdminPortal.Ocelot.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<ProductApiClient> _logger;
        private readonly UrlsConfig _urls;

        public ProductApiClient(HttpClient httpClient, ILogger<ProductApiClient> logger, IOptions<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<ProductItem> GetItemByIdAsync(int id)
        {
            _logger.LogDebug("HttpClient-Product-GetItemByIdAsync created, id={@id}", id);
            var url = $"{_urls.Product}{UrlsConfig.ProductOperations.GetItemById(id)}";
            //var content = new StringContent(JsonSerializer.Serialize(basket), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.GetAsync(url);
            
            _logger.LogDebug("HttpClient-Product-GetItemByIdAsync response: {@response}", response);
            
            response.EnsureSuccessStatusCode();

            var productResponse = await response.Content.ReadAsStringAsync();
            
            var productItem = JsonSerializer.Deserialize<ProductItem>(productResponse, JsonDefaults.CaseInsensitiveOptions);
            _logger.LogDebug("HttpClient-Product-GetItemByIdAsync Deserialize: {@productItem}", productItem);

            return productItem;
        }

        public Task UpdateAsync(ProductItem item)
        {
            throw new NotImplementedException();
        }
    }
}
