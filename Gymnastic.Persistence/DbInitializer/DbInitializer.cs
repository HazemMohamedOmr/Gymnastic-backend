
using Gymnastic.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Gymnastic.Persistence.DbInitializer
{
    public class DbInitialize
    {
        public static void Initialize(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { RoleConstants.Admin, RoleConstants.Customer, RoleConstants.Coach };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }
        }
    }
}
