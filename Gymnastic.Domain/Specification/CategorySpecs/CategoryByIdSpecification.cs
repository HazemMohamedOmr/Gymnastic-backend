using Gymnastic.Domain.Models;

namespace Gymnastic.Domain.Specification.CategorySpecs
{
    public class CategoryByIdSpecification : BaseSpecification<Category, int>
    {
        public CategoryByIdSpecification(int id) : base(c => c.Id == id)
        {
        }
    }
}
