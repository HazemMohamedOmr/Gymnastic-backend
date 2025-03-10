using Gymnastic.Domain.Models;

namespace Gymnastic.Application.Dto.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public int Stock { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
