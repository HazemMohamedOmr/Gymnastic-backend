using System.Linq.Expressions;

namespace Gymnastic.Domain.Specification
{
    public interface ISpecification<T, TId>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, bool>> CountCriteria { get; }
        Expression<Func<T, decimal>> SumSelector { get; }
        Expression<Func<T, decimal>> MaxSelector { get; }
        Expression<Func<T, decimal>> MinSelector { get; }
        Expression<Func<T, decimal>> AvgSelector { get; }
        Expression<Func<T, bool>> AnyCriteria { get; }
        Expression<Func<T, bool>> AllCriteria { get; }
        int? Take { get; }
        int? Skip { get; }
        bool AsNoTracking { get; }
        bool AsSplitQuery { get; }
    }

}
