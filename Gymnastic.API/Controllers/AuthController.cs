using Gymnastic.API.APIEndpoints;
using Gymnastic.Application.UseCases.Auth.Commands.LoginCommand;
using Gymnastic.Application.UseCases.Auth.Commands.RegisterCommand;
using Gymnastic.Application.UseCases.Auth.Commands.SendEmailVerificationCommand;
using Gymnastic.Application.UseCases.Auth.Commands.VerifyEmailCommand;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gymnastic.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(ApiEndpoints.Auth.Register)]
        public async Task<IActionResult> Register(RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if(result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return CreatedAtAction(nameof(Register), new { Email = command.Email}, result.Data);
        }

        [HttpPost(ApiEndpoints.Auth.Login)]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return Ok(result.Data);
        }

        [HttpGet(ApiEndpoints.Auth.VerifyEmail)]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string token, CancellationToken cancellationToken)
        {
            var command = new VerifyEmailCommand
            {
                Email = email,
                Token = token
            };
            var result = await _mediator.Send(command, cancellationToken);
            if(result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return Ok(result.Data);
        }

        [HttpPost(ApiEndpoints.Auth.SendEmailVerificaiton)]
        public async Task<IActionResult> SendEmailVerificaiton(SendEmailVerificationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess is false)
                return StatusCode(result.StatusCode, ProblemFactory.CreateProblemDetails(HttpContext, result.StatusCode, result.Message, result.Errors));
            return Ok(result.Data);
        }
    }
}
