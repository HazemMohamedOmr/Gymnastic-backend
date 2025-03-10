using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Cart : BaseEntity<int>
    {
        public required string UserId { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}