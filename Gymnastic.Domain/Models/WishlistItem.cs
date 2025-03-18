using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class WishlistItem : BaseEntity<int>
    {
        public required int WishlistId { get; set; }
        public required int ProductId { get; set; }
        public Wishlist Wishlist { get; set; }
        public Product Product { get; set; }
    }
}