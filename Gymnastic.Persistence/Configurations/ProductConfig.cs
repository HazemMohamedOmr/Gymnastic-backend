using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.HasMany(p => p.Images).WithOne(pi => pi.Product).HasForeignKey(pi => pi.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}