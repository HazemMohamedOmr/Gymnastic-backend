using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> entity)
        {
            entity.HasKey(ci => ci.Id);
            entity.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}