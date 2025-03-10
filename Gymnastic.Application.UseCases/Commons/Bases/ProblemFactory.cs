using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Gymnastic.Application.UseCases.Commons.Bases
{
    public static class ProblemFactory
    {
        public static ProblemDetails CreateProblemDetails(HttpContext httpContext, int statusCode, string detail)
        {
            return CreateProblemDetails(httpContext, statusCode, detail, null);
        }

        public static ProblemDetails CreateProblemDetails(HttpContext httpContext, int statusCode, object errors)
        {
            return CreateProblemDetails(httpContext, statusCode, null, errors);
        }

        public static ProblemDetails CreateProblemDetails(HttpContext httpContext, int statusCode, string? detail, object? errors)
        {
            var problemDetails = new ProblemDetails
            {
                Detail = detail,
                Instance = httpContext.Request.Path,
                Status = statusCode,
                Title = ReasonPhrases.GetReasonPhrase(statusCode)
            };

            if (errors != null) problemDetails.Extensions["errors"] = errors;

            return problemDetails;
        }
    }
}
