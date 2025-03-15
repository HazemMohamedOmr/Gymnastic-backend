using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Cart : BaseEntity<int>, ISoftDeletable
    {
        public required string UserId { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}