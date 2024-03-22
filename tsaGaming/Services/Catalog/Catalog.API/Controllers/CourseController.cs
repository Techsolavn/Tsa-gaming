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
    public class CourseController : ControllerBase
    {

        private readonly IMediator _mediator;

        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<CourseDTO>>> Get()
        {
            _logger.LogInformation("course controller - get courses");
            var result = await _mediator.Send(new GetCoursesQuery { });
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
