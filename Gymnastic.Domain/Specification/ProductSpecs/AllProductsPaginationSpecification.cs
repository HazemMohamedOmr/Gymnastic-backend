using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.ProductSpecs
{
    public class AllProductsPaginationSpecification : BaseSpecification<Product, int>
    {
        public AllProductsPaginationSpecification(int skip, int take)
        {
            AddInclude(x => x.Category);
            ApplyPaging(skip, take);
        }
    }
}
