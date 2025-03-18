namespace Gymnastic.Application.Dto.DTOs
{
    public class WishlistItemDTO
    {
        public int Id { get; set; }
        public required int WishlistId { get; set; }
        public required int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
