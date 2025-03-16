using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Gymnastic.Application.UseCases.Commons.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = Stopwatch.StartNew();
            async Task<TResponse> WrappedNext()
            {
                var response = await next();
                timer.Stop();
                return response;
            }
            var response = await WrappedNext();
            var elapsedMs = timer.ElapsedMilliseconds;
            if (elapsedMs > 50)
            {
                _logger.LogWarning("Request {RequestName} took {ElapsedMs} ms - Slow", typeof(TRequest).Name, elapsedMs);
                var logMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} - Request {typeof(TRequest).Name} took {elapsedMs} ms\n";
                await File.AppendAllTextAsync("PerformanceLogs.log", logMessage, cancellationToken);
            }
            _logger.LogInformation("Request {RequestName} took {ElapsedMs} ms", typeof(TRequest).Name, elapsedMs);
            return response;
        }
    }
}
