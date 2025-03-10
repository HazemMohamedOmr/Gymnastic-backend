using Gymnastic.Domain.Models;

namespace Gymnastic.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product, int> Product { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        // Transactions
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync();
    }
}
