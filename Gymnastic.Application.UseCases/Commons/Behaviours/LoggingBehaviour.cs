using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gymnastic.Application.UseCases.Commons.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Gymnastic Request Handling: { name } {@request }", typeof(TRequest).Name, JsonConvert.SerializeObject(request));
            var response = await next();
            _logger.LogInformation("Gymnastic Response Handling: { name } {@response }", typeof(TResponse).Name, JsonConvert.SerializeObject(request));

            return response;
        }
    }
}
