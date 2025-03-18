using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.CartSpecs
{
    public class CartByUserIdSpecification : BaseSpecification<Cart, int>
    {
        public CartByUserIdSpecification(string id) : base(c => c.UserId == id)
        {
        }
    }
}
