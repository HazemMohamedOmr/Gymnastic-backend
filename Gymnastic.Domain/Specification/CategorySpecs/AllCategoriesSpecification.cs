using Gymnastic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification.CategorySpecs
{
    public class AllCategoriesSpecification : BaseSpecification<Category, int>
    {
        public AllCategoriesSpecification(
            string? searchTerm = null,
            int? pageNumber = null,
            int? pageSize = null,
            string? orderBy = null,
            bool isDecending = false)
        {
            Expression<Func<Category, bool>> criteria = p => true;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                criteria = criteria.AndAlso(p => p.Name.Contains(searchTerm));
            }

            Where(criteria);

            // Sorting
            if (!string.IsNullOrEmpty(orderBy))
            {
                var property = typeof(Category).GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, orderBy, StringComparison.OrdinalIgnoreCase));
                if (isDecending)
                {
                    AddOrderByDescending(p => EF.Property<Category>(p, orderBy));
                }
                else
                {
                    AddOrderBy(p => EF.Property<Category>(p, orderBy));
                }
            }

            // Pagination
            if (pageNumber is not null && pageSize is not null)
            {
                ApplyPaging(((int)pageNumber - 1) * (int)pageSize, (int)pageSize);
            }
        }

    }
}
