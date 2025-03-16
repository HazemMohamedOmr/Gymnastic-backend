using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.ProductImagesSpecs
{
    public class GetAllProductImagesByProductId : BaseSpecification<ProductImage, int>
    {
        public GetAllProductImagesByProductId(int id) : base(pi => pi.ProductId == id)
        {
        }
    }
}
