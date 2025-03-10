﻿using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class Wishlist : BaseEntity<int>
    {
        public required string UserId { get; set; }
        public ICollection<WishlistItem>? WishlistItems { get; set; }
    }
}