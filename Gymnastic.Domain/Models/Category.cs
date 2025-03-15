using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Category : BaseEntity<int>, ISoftDeletable
    {
        public required string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}