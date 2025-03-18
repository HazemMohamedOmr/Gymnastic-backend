namespace Gymnastic.Application.Dto.DTOs
{
    public class CartItemsDTO
    {
        public int Id { get; set; }
        public required int CartId { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}
