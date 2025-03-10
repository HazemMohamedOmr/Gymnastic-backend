using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Application.UseCases.Commons.Exceptions;
using System.Text.Json;

namespace Gymnastic.API.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = ProblemFactory.CreateProblemDetails(
                    context,
                    context.Response.StatusCode,
                    "One or more validation errors occurred.",
                    ex.Errors
                );
                await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
            }
        }
    }
}
