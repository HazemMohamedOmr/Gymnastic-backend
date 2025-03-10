using Gymnastic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymnastic.Domain.Models
{
    public class WishlistItem : BaseEntity<int>
    {
        public required int WishlistId { get; set; }
        public required int ProductId { get; set; }
        public Wishlist Wishlist { get; set; }
        public Product Product { get; set; }
    }
}