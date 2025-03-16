using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class WishlistItemConfig : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> entity)
        {
            entity.HasKey(wi => wi.Id);
            entity.HasOne(wi => wi.Product).WithMany().HasForeignKey(wi => wi.ProductId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}