using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class OrderDetail : BaseEntity<int>
    {
        public required int OrderId { get; set; }
        public required int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}