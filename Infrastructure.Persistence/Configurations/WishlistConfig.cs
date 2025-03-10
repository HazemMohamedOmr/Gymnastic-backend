using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class WishlistConfig : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> entity)
        {
            entity.HasKey(w => w.Id);
            entity.HasMany(w => w.WishlistItems).WithOne(wi => wi.Wishlist).HasForeignKey(wi => wi.WishlistId);
            entity.HasOne<ApplicationUser>().WithOne().HasForeignKey<Wishlist>(c => c.UserId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}