using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gymnastic.Domain.Specification.CartSpecs
{
    public class CartByUserIdWithAllSpecifications : BaseSpecification<Cart, int>
    {
        public CartByUserIdWithAllSpecifications(string id) : base(c => c.UserId == id)
        {
            AddIncludeExpression(e => e
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Category));

            AddIncludeExpression(e => e
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.Images));

            EnableSplitQuery(); // TODO BenchMark it

        }
    }
}
