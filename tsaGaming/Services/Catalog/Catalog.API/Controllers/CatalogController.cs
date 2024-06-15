using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Application.Commands;
using Catalog.API.Application.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Services.Common.Dto;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly IMediator _mediator;

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<CatalogDTO>>> Get([FromQuery] GetCatalogsQuery getCatalogsQuery)
        {
            _logger.LogInformation("catalog controller - get catalogs");
            var result = await _mediator.Send(getCatalogsQuery);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
