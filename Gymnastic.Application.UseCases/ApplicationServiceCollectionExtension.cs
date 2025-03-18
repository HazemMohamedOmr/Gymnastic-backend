using FluentValidation;
using Gymnastic.Application.Interface.Services;
using Gymnastic.Application.UseCases.Commons.Behaviours;
using Gymnastic.Application.UseCases.Commons.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gymnastic.Application.UseCases
{
    public static class ApplicationServiceCollectionExtension
    {
        public static void ConfigureApplicationDependcies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TrimPropertiesBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddScoped<ISendEmailService, SendEmailService>();
        }
    }
}
