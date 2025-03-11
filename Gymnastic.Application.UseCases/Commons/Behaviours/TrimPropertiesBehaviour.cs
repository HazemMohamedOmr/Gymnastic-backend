using MediatR;

namespace Gymnastic.Application.UseCases.Commons.Behaviours
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotTrimAttribute : Attribute { }

    public class TrimPropertiesBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var properties = typeof(TRequest).GetProperties()
                .Where(p => p.PropertyType == typeof(string) &&
                           p.CanWrite &&
                           !Attribute.IsDefined(p, typeof(DoNotTrimAttribute)));

            foreach (var prop in properties)
            {
                var value = (string)prop.GetValue(request);
                if (value != null)
                {
                    prop.SetValue(request, value.Trim());
                }
            }

            return await next();
        }
    }
}
