using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.CartSpecs
{
    public class CartByUserIdWithCartItemsSpecifications : BaseSpecification<Cart, int>
    {
        public CartByUserIdWithCartItemsSpecifications(string id) : base(c => c.UserId == id)
        {
            AddInclude(c => c.CartItems);
        }
    }
}
