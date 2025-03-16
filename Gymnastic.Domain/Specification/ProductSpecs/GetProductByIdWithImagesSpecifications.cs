using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.ProductSpecs
{
    public class GetProductByIdWithImagesSpecifications : BaseSpecification<Product, int>
    {
        public GetProductByIdWithImagesSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Images);
        }
    }
}
