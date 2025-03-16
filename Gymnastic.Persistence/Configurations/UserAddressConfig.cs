using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> entity)
        {
            entity.HasKey(ua => ua.Id);
            entity.Property(ua => ua.Street).IsRequired().HasMaxLength(200);
            entity.Property(ua => ua.City).IsRequired().HasMaxLength(100);
            entity.Property(ua => ua.State).HasMaxLength(100);
            entity.Property(ua => ua.ZipCode).HasMaxLength(20);
            entity.Property(ua => ua.Country).IsRequired().HasMaxLength(100);
            entity.HasOne(u => u.User).WithMany(u => u.UserAddresses).HasForeignKey(ua => ua.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(ua => ua.MainAddressUser)
                  .WithOne(au => au.MainAddress)
                  .HasForeignKey<ApplicationUser>(au => au.MainAddressId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}