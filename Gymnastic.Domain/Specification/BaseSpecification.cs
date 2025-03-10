using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification
{
    public class BaseSpecification<T, TId> : ISpecification<T, TId>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, bool>> CountCriteria { get; private set; }
        public Expression<Func<T, decimal>> SumSelector { get; private set; }
        public Expression<Func<T, decimal>> MaxSelector { get; private set; }
        public Expression<Func<T, decimal>> MinSelector { get; private set; }
        public Expression<Func<T, decimal>> AvgSelector { get; private set; }
        public Expression<Func<T, bool>> AnyCriteria { get; private set; }
        public Expression<Func<T, bool>> AllCriteria { get; private set; }
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
        public void ApplyCount(Expression<Func<T, bool>> countSelector) => CountCriteria = countSelector;
        public void ApplySum(Expression<Func<T, decimal>> sumSelector) => SumSelector = sumSelector;
        public void ApplyMax(Expression<Func<T, decimal>> maxSelector) => MaxSelector = maxSelector;
        public void ApplyMin(Expression<Func<T, decimal>> minSelector) => MinSelector = minSelector;
        public void ApplyAvg(Expression<Func<T, decimal>> avgSelector) => AvgSelector = avgSelector;
        public void ApplyAny(Expression<Func<T, bool>> anySelector) => AnyCriteria = anySelector;
        public void ApplyAll(Expression<Func<T, bool>> allSelector) => AllCriteria = allSelector;
    }
}
