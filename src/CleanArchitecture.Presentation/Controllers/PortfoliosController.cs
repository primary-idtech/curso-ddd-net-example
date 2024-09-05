using CleanArchitecture.Application.Portfolios.Create;
using CleanArchitecture.Application.Portfolios.Delete;
using CleanArchitecture.Application.Portfolios.FindAll;
using CleanArchitecture.Application.Portfolios.FindOne;
using CleanArchitecture.Application.Portfolios.Update;
using CleanArchitecture.Presentation.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers
{
    /// <summary>
    /// Portfolio Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        /// <summary>
        /// Find All
        /// </summary>
        /// <remarks>
        /// Returns paginated Portfolios results. 
        /// The results are sorted by the field and direction specified in the sort parameter. 
        /// The offset parameter specifies the starting point to return results. 
        /// The limit parameter specifies the maximum number of results to return. 
        /// The default values are offset=0 and limit=200.
        ///
        /// Sample request:
        ///
        ///     GET /api/portfolios/findAll?sort=id,desc&amp;offset=0&amp;limit=200
        /// </remarks>
        /// <response code="200">Request successful</response>
        /// <response code="401">The request is not validly authenticated</response>
        /// <response code="403">The client is not authorized for using this operation</response>
        /// <response code="404">The resource was not found</response>
        [HttpGet("findAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortfolioPageResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 5)]
        public async Task<IActionResult> GetAll([FromQuery] PortfolioPageRequest req)
        {
            var query = new FindAllQuery()
            {
                Enabled = req.Enabled ?? true,
                Limit = req.Limit,
                Offset = req.Offset,
                Sort = req.Sort
            };
            var pageDto = await this.mediator.Send(query);

            return this.Ok(new PortfolioPageResponse(pageDto));
        }

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id" example="1">Identifier from Portfolio</param>
        /// <returns>Information result for one Portfolio</returns>
        /// <remarks>
        /// Returns one Portfolio according to ID.
        /// 
        /// Sample request:
        ///
        ///     GET /api/portfolios/1
        /// </remarks>
        /// <response code="200">Request successful</response>
        /// <response code="401">The request is not validly authenticated</response>
        /// <response code="403">The client is not authorized for using this operation</response>
        /// <response code="404">The resource was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortfolioResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 5)]
        public async Task<IActionResult> Get(long id)
        {
            var query = new FindOneByIdQuery() { Id = id };
            var dto = await this.mediator.Send(query);
            if (dto == null) return this.NotFound();

            return this.Ok(new PortfolioResponse(dto));
        }

        /// <summary>
        /// Create Portfolio
        /// </summary>
        /// <returns>Information resulting from the creation portfolio</returns>
        /// <remarks>
        /// Create a new Portfolio.
        /// 
        /// Sample request:
        ///
        ///     POST /api/portfolios
        ///     {
        ///         "name": "Cartera1"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="401">The request is not validly authenticated</response>
        /// <response code="403">The client is not authorized for using this operation</response>
        /// <response code="404">The resource was not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortfolioResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreatePortfolioRequest req)
        {
            var cmd = new CreateCommand { Name = req.Name };

            var portfolio = await this.mediator.Send(cmd);
            return this.Created($"/api/portfolios/{portfolio.Id}", new PortfolioResponse(portfolio));
        }

        /// <summary>
        /// Delete Portfolio 
        /// </summary>
        /// <remarks>
        /// Delete a Portfolio by ID.
        ///
        /// Sample request: 
        /// 
        ///     DELETE /api/portfolios/{id}
        /// </remarks>
        /// <response code="200">Request successful</response>
        /// <response code="401">The request is not validly authenticated</response>
        /// <response code="403">The client is not authorized for using this operation</response>
        /// <response code="404">The resource was not found</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var cmd = new DeleteByIdCommand { Id = id };
            await this.mediator.Send(cmd);
            return this.Ok();
        }

        /// <summary>
        /// Update Portfolio
        /// </summary>
        /// <remarks>
        /// Update a Portfolio by ID.
        ///
        /// Sample request: 
        /// 
        ///     PUT /api/portfolios/{id}
        ///     {
        ///         "name": "new Name",
        ///         "enabled": false
        ///     }
        /// </remarks>
        /// <response code="200">Request successful</response>
        /// <response code="401">The request is not validly authenticated</response>
        /// <response code="403">The client is not authorized for using this operation</response>
        /// <response code="404">The resource was not found</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdatePortfolioRequest req)
        {
            await this.mediator.Send(new UpdateCommand { 
                Id = id,
                Name = req.Name,
                Enabled = req.Enabled
            });

            return this.Ok();
        }
    }
}
