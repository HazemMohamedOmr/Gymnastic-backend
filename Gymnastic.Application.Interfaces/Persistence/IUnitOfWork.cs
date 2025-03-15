using Gymnastic.Domain.Models;

namespace Gymnastic.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product, int> Product { get; }
        IRepository<Category, int> Category { get; }
        IRepository<Cart, int> Cart { get; }
        IRepository<CartItem, int> CartItem { get; }
        IRepository<Order, int> Order { get; }
        IRepository<OrderDetail, int> OrderDetails { get; }
        IRepository<ProductImage, int> ProductImage { get; }
        IRepository<UserAddress, int> UserAddress { get; }
        IRepository<Wishlist, int> Wishlist { get; }
        IRepository<WishlistItem, int> WishlistItem { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        // Transactions
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync();
    }
}
