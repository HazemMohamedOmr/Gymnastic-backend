﻿using Gymnastic.Domain.Common;

namespace Gymnastic.Domain.Models
{
    public class ProductImage : BaseEntity<int>, IImageCloud, ISoftDeletable
    {
        public required string ImageUrl { get; set; }
        public required string PublicId { get; set; }
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
