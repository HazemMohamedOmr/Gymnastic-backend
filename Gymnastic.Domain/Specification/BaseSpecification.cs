using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null) return expr2;
            if (expr2 == null) return expr1;

            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
    public class BaseSpecification<T, TId> : ISpecification<T, TId>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
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
        public void Where(Expression<Func<T, bool>> criteria) => Criteria = criteria;
        public void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression) => OrderByDescending = orderByDescExpression;
        public void ApplyPaging(int skip, int take) { Skip = skip; Take = take; }
    }
}
