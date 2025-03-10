using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Order : BaseEntity<int>
    {
        public required string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}