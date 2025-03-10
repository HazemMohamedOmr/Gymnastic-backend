using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Category : BaseEntity<int>
    {
        public required string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}