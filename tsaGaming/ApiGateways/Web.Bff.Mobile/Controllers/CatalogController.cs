using Microsoft.AspNetCore.Mvc;
using Web.Bff.AdminPortal.Models;
using Web.Bff.Mobile.Services.Interfaces;

namespace Web.Bff.Mobile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogApiClient _catalogApiClient;

        public CatalogController(ILogger<CatalogController> logger, ICatalogApiClient catalogApiClient)
        {
            _logger = logger;
            _catalogApiClient = catalogApiClient;
        }

        [HttpGet()]
        public async Task<IEnumerable<CatalogDTO>> GetAll()
        {
            return await _catalogApiClient.GetAllAsync();
        }
    }
}