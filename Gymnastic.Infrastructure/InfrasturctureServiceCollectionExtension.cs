using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Infrastructure.Authentication;
using Gymnastic.Infrastructure.BackgroundJobs;
using Gymnastic.Infrastructure.Helpers;
using Gymnastic.Infrastructure.Mail;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gymnastic.Infrastructure
{
    public static class InfrasturctureServiceCollectionExtension
    {
        public static void ConfigureInfrasturctureDependcies(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IBackgroundJobService, HangfireService>();

            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
        }

        public static void AddHangfireDashboard(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            // Map JWT Helper Class to JWT in appsettings
            services.Configure<JWT>(Configuration.GetSection("JWT"));

            // Registering JWT Service
            services.AddScoped<IJWTService, JWTService>();

            // JWT Configs
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero // TODO : ClockSkew is Optional!
                    };
                });
        }

        public static void ConfigureMail(this IServiceCollection services, IConfiguration Configuration)
        {
            // Map MailSettings Helper Class to MailSettings in appsettings
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            // Registering EmailService Service
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}