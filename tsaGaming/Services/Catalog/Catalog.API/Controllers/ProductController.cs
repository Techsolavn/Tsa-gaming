using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Application.Commands;
using Catalog.API.Application.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("{id:int}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            _logger.LogInformation("product controller - get product: {@result}", id);
            var result = await _mediator.Send(new GetProductQuery { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> Update(int id, [FromBody] CreateProductCommand product)
        {
            _logger.LogInformation("product controller - Update: {@result}", product);
            
            product.Id = id;
            var result = await _mediator.Send(product);
            
            if (!result) return BadRequest();
            
            return Ok(result);
        }
    }
}
