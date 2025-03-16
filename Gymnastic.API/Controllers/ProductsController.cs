using Gymnastic.API.APIEndpoints;
using Gymnastic.Application.Dto.Contracts.Requests;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Application.UseCases.Products.Commands.CreateProductCommand;
using Gymnastic.Application.UseCases.Products.Queries.GetAllProductsQuery;
using Gymnastic.Application.UseCases.Products.Queries.GetByIdProductQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gymnastic.API.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(ApiEndpoints.Products.Get)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdProductQuery {Id = id}, cancellationToken);
            if(result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message));
            return Ok(result.Data);
        }

        [HttpGet(ApiEndpoints.Products.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery]GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result.Data);
        }
        [HttpPost(ApiEndpoints.Products.Create)]
        public async Task<IActionResult> Create([FromForm]CreateProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateProductCommand(request), cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message));
            return CreatedAtAction(nameof(Create), new { Id = result.Data!.Id }, result.Data);
        }
    }
}
