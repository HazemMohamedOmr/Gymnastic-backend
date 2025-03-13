using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.ProductSpecs
{
    public class ProductByIdWithCategorySpecification : BaseSpecification<Product, int>
    {
        public ProductByIdWithCategorySpecification(int productId) : base(p => p.Id == productId)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Images);
        }
    }
}
