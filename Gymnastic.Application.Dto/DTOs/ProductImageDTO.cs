namespace Gymnastic.Application.Dto.DTOs
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
