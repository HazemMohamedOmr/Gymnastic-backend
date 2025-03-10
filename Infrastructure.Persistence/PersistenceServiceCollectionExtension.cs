using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Domain.Models;
using Gymnastic.Persistence.Data;
using Gymnastic.Persistence.DbInitializer;
using Gymnastic.Persistence.Repositories;
using Gymnastic.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gymnastic.Persistence
{
    public static class PersistenceServiceCollectionExtension
    {
        public static void ConfigurePersistenceDependcies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // Register DbContext
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
        }

        // Register Identity
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        // Seed Dummy Data
        public static void SeedDummyData(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<DataSeeder>>();

            // Seed Data
            DataSeeder.Seed(dbContext, userManager, logger);
        }

        // Initalize Database
        public static void DatabaseInitialize(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            DbInitialize.Initialize(roleManager);
        }
    }
}