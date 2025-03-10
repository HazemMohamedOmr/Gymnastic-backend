using Gymnastic.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Gymnastic.Domain.Models
{
    public class ApplicationUser : IdentityUser, IBaseEntity<string>, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? MainAddressId { get; set; }
        public UserAddress? MainAddress { get; set; }
        public ICollection<UserAddress>? UserAddresses { get; set; }

        // Implementing IBaseEntity properties
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}