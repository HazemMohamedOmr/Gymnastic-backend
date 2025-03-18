using Gymnastic.API.APIEndpoints;
using Gymnastic.Application.UseCases.Carts.Commands.AddToCartCommand;
using Gymnastic.Application.UseCases.Carts.Commands.DeleteCartItemCommand;
using Gymnastic.Application.UseCases.Carts.Commands.UpdateCartItemQuantityCommand;
using Gymnastic.Application.UseCases.Carts.Queries.GetUserCartQuery;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gymnastic.API.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(ApiEndpoints.Customer.GetCartItems)]
        public async Task<IActionResult> GetCartItems(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserCartQuery(), cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return Ok(result.Data);
        }

        [HttpPost(ApiEndpoints.Customer.AddCartItem)]
        public async Task<IActionResult> AddCartItem([FromBody]AddToCartCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return CreatedAtAction(nameof(AddCartItem), new { id = result.Data!.Id }, result.Data);
        }

        [HttpDelete(ApiEndpoints.Customer.DeleteCartItem)]
        public async Task<IActionResult> DeleteCartItem([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCartItemCommand { Id = id }, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return NoContent();
        }

        [HttpPatch(ApiEndpoints.Customer.UpdateCartItemQuantity)]
        public async Task<IActionResult> UpdateCartItemQuantity([FromRoute] int id, UpdateCartItemQuantityCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return Ok(result.Data);
        }
    }
}
