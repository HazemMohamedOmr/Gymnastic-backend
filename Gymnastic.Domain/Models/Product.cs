using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Product : BaseEntity<int>, ISoftDeletable
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public int Stock { get; set; }
        public required int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage>? Images { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}