using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class UserAddress : BaseEntity<int>, ISoftDeletable
    {
        public required string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser MainAddressUser { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public required string Country { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}