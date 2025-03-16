using Microsoft.AspNetCore.Http;

namespace Gymnastic.Application.Dto.Contracts.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
