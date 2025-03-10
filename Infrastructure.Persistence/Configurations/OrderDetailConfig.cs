using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymnastic.Persistence.Configurations
{
    internal class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> entity)
        {
            entity.HasKey(od => od.Id);
            entity.Property(o => o.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            entity.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            entity.HasOne(od => od.Product).WithMany().HasForeignKey(od => od.ProductId).IsRequired(); ;
        }
    }
}