using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class CartItem : BaseEntity<int>
    {
        public required int CartId { get; set; }
        public required int ProductId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}