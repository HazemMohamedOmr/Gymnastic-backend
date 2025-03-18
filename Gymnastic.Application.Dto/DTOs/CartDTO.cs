namespace Gymnastic.Application.Dto.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public ICollection<CartItemsDTO>? CartItems { get; set; }
    }
}
