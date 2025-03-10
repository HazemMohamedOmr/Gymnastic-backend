using Gymnastic.Domain.Common;
using Gymnastic.Domain.Specification;
using System.Linq.Expressions;

namespace Gymnastic.Application.Interface.Persistence
{
    public interface IRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetEntityWithSpec(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> ListAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, Expression<Func<T, bool>>? predicate = null);
        Task<decimal> MaxAsync(Expression<Func<T, decimal>> selector, Expression<Func<T, bool>>? predicate = null);
        Task<decimal> MinAsync(Expression<Func<T, decimal>> selector, Expression<Func<T, bool>>? predicate = null);
        Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector, Expression<Func<T, bool>>? predicate = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate);

        //Task<int> CountAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        //Task<decimal> SumAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        //Task<decimal> MaxAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        //Task<decimal> MinAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        //Task<decimal> AvgAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        //Task<bool> AnyAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);
        //Task<bool> AllAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default);

    }

}
