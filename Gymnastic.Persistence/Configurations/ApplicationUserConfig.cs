using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.Property(au => au.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(au => au.LastName).IsRequired().HasMaxLength(100);
            entity.HasMany(au => au.UserAddresses).WithOne(ua => ua.User).HasForeignKey(ua => ua.UserId);
            entity.HasOne(au => au.MainAddress).WithOne(ua => ua.MainAddressUser).HasForeignKey<ApplicationUser>(ua => ua.MainAddressId);
        }
    }
}