using Microsoft.Extensions.Options;
using System.Text.Json;
using Web.Bff.AdminPortal.Models;
using Web.Bff.Mobile.Services.Interfaces;

namespace Web.Bff.Mobile.Services
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<CatalogApiClient> _logger;
        private readonly UrlsConfig _urls;

        public CatalogApiClient(HttpClient httpClient, ILogger<CatalogApiClient> logger, IOptions<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<IEnumerable<CatalogDTO>> GetAllAsync()
        {
            _logger.LogDebug("HttpClient-Catalog-GetAllAsync created");
            var url = $"{_urls.Catalog}{UrlsConfig.CatalogOperations.GetAll()}";
            var response = await _apiClient.GetAsync(url);
            
            _logger.LogDebug("HttpClient-Catalog-GetAllAsync response: {@response}", response);
            
            response.EnsureSuccessStatusCode();

            var catalogResponse = await response.Content.ReadAsStringAsync();
            
            var catalogs = JsonSerializer.Deserialize<IEnumerable<CatalogDTO>>(catalogResponse, JsonDefaults.CaseInsensitiveOptions);
            _logger.LogDebug("HttpClient-Catalog-GetAllAsync deserialize: {@catalogs}", catalogs);

            if (catalogs != null)
            {
                return catalogs;
            }
            return new List<CatalogDTO>();
        }
    }
}
