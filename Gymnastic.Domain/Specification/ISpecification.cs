using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification
{
    public interface ISpecification<T, TId>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> IncludeExpressions { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int? Take { get; }
        int? Skip { get; }
        bool AsNoTracking { get; }
        bool AsSplitQuery { get; }
    }

}
