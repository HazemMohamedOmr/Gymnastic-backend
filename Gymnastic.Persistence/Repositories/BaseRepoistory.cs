using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Domain.Common;
using Gymnastic.Domain.Specification;
using Gymnastic.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Linq;

namespace Gymnastic.Persistence.Repositories
{
    public class BaseRepoistory<T, TId> : IRepository<T, TId> where T : BaseEntity<TId>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepoistory(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }

        // ---------------------- AGGREGATE FUNCTIONS ----------------------

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null ? _dbSet.CountAsync() : _dbSet.CountAsync(predicate));
        }

        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector,
            Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null ? _dbSet.SumAsync(selector) : _dbSet.Where(predicate).SumAsync(selector));
        }

        public async Task<decimal> MaxAsync(Expression<Func<T, decimal>> selector,
            Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null ? _dbSet.MaxAsync(selector) : _dbSet.Where(predicate).MaxAsync(selector));
        }

        public async Task<decimal> MinAsync(Expression<Func<T, decimal>> selector,
            Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null ? _dbSet.MinAsync(selector) : _dbSet.Where(predicate).MinAsync(selector));
        }

        public async Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector,
            Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null
                ? _dbSet.AverageAsync(selector)
                : _dbSet.Where(predicate).AverageAsync(selector));
        }

        // ---------------------- LOGICAL OPERATIONS ----------------------

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null ? _dbSet.AnyAsync() : _dbSet.AnyAsync(predicate));
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AllAsync(predicate);
        }

        // ---------------------- CUD OPERATIONS ----------------------

        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _dbSet.AddAsync(entity);
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.UpdatedAt = DateTime.UtcNow;

            _dbSet.UpdateRange(entities);
            return entities;
        }

        public void Delete(T entity)
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _dbSet.Remove(entity); // Hard delete if not soft-deletable
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is ISoftDeletable softDeletableEntity)
                {
                    softDeletableEntity.IsDeleted = true;
                    entity.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    _dbSet.Remove(entity); // Hard delete if not soft-deletable
                }
            }
        }

        //public async Task<int> CountAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    var query = ApplySpecification(spec);
        //    return spec.CountCriteria != null
        //        ? await query.CountAsync(spec.CountCriteria, cancellationToken)
        //        : await query.CountAsync(cancellationToken);
        //}

        //public async Task<decimal> SumAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    var query = ApplySpecification(spec);
        //    return spec.SumSelector != null
        //        ? await query.SumAsync(spec.SumSelector, cancellationToken)
        //        : 0;
        //}

        //public async Task<decimal> MaxAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    var query = ApplySpecification(spec);
        //    return spec.MaxSelector != null
        //        ? await query.MaxAsync(spec.SumSelector, cancellationToken)
        //        : 0;
        //}

        //public async Task<decimal> MinAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    var query = ApplySpecification(spec);
        //    return spec.MinSelector != null
        //        ? await query.MinAsync(spec.MinSelector, cancellationToken)
        //        : 0;
        //}

        //public async Task<decimal> AvgAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    var query = ApplySpecification(spec);
        //    return spec.AvgSelector != null
        //        ? await query.AverageAsync(spec.AvgSelector, cancellationToken)
        //        : 0;
        //}

        //public async Task<bool> AnyAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    var query = ApplySpecification(spec);
        //    return spec.AnyCriteria != null
        //        ? await query.AnyAsync(spec.AnyCriteria, cancellationToken)
        //        : await query.AnyAsync(cancellationToken);
        //}

        //public async Task<bool> AllAsync(ISpecification<T, TId> spec, CancellationToken cancellationToken = default)
        //{
        //    return await ApplySpecification(spec).AllAsync(spec.AllCriteria, cancellationToken);
        //}

        private IQueryable<T> ApplySpecification(ISpecification<T, TId> spec)
        {
            var query = _context.Set<T>().AsQueryable();

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            // Apply Include-ThenInclude chains if any
            if (spec.IncludeExpressions != null)
                query = spec.IncludeExpressions(query);

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);

            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);

            if (spec.AsSplitQuery)
                query = query.AsSplitQuery();

            if (spec.AsNoTracking)
                query = query.AsNoTracking();
            return query;
        }

    }
}
