using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> entity)
        {
            entity.HasKey(pi => pi.Id);
            entity.Property(pi => pi.PublicId).IsRequired();
            entity.Property(pi => pi.ImageUrl).IsRequired();
            entity.HasOne(pi => pi.Product).WithMany(p => p.Images).HasForeignKey(pi => pi.ProductId);
        }
    }
}
