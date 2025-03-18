namespace Gymnastic.Application.Dto.DTOs
{
    public class WishlistDTO
    {
        public required string UserId { get; set; }
        public ICollection<WishlistItemDTO>? WishlistItems { get; set; }
    }
}
