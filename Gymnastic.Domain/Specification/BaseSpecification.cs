using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification
{
    public class BaseSpecification<T, TId> : ISpecification<T, TId>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int? Take { get; private set; }
        public int? Skip { get; private set; }
        public bool AsNoTracking { get; private set; } = false;
        public bool AsSplitQuery { get; private set; } = false;

        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void EnableNoTracking() => AsNoTracking = true;
        public void EnableSplitQuery() => AsSplitQuery = true;
        public void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression) => OrderByDescending = orderByDescExpression;
        public void ApplyPaging(int skip, int take) { Skip = skip; Take = take; }
    }
}
