using Gymnastic.API.APIEndpoints;
using Gymnastic.Application.UseCases.Categories.Commands.CreateCategoryCommand;
using Gymnastic.Application.UseCases.Categories.Commands.DeleteCategoryCommand;
using Gymnastic.Application.UseCases.Categories.Commands.UpdateCategoryCommand;
using Gymnastic.Application.UseCases.Categories.Queries.GetAllCategoriesQuery;
using Gymnastic.Application.UseCases.Categories.Queries.GetByIdCategoryQuery;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gymnastic.API.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(ApiEndpoints.Category.Get)]
        public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdCategoryQuery { Id = id }, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message!));
            return Ok(result.Data);
        }

        [HttpGet(ApiEndpoints.Category.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery]GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message!));
            return Ok(result.Data);
        }

        [HttpPost(ApiEndpoints.Category.Create)]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message!));
            return CreatedAtAction(nameof(Create), new { id = result.Data!.Id }, result.Data);
        }


        [HttpPut(ApiEndpoints.Category.Update)]
        public async Task<IActionResult> Update([FromBody]UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message!));
            return Ok(result.Data);
        }

        [HttpDelete(ApiEndpoints.Category.Delete)]
        public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = id }, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message!));
            return NoContent();
        }
    }
}
