using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.ProductSpecs
{
    public class AllProductsSpecification : BaseSpecification<Product, int>
    {
        public AllProductsSpecification()
        {
            AddInclude(x => x.Category);
        }
    }
}
