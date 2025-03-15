using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.ProductSpecs
{
    public class ProductByIdSpecification : BaseSpecification<Product, int>
    {
        public ProductByIdSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Images);
        }
    }
}
