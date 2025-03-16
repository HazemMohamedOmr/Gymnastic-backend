using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Domain.Models;
using Gymnastic.Persistence.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gymnastic.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;

        private IRepository<Product, int> _product;
        private IRepository<Category, int> _category;
        private IRepository<Cart, int> _cart;
        private IRepository<CartItem, int> _cartItem;
        private IRepository<Order, int> _order;
        private IRepository<OrderDetail, int> _orderDetails;
        private IRepository<ProductImage, int> _productImage;
        private IRepository<UserAddress, int> _userAddress;
        private IRepository<Wishlist, int> _wishlist;
        private IRepository<WishlistItem, int> _wishlistItem;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Product, int> Product => _product ??= new BaseRepoistory<Product, int>(_context);
        public IRepository<Category, int> Category => _category ??= new BaseRepoistory<Category, int>(_context);
        public IRepository<Cart, int> Cart => _cart ??= new BaseRepoistory<Cart, int>(_context);
        public IRepository<CartItem, int> CartItem => _cartItem ??= new BaseRepoistory<CartItem, int>(_context);
        public IRepository<Order, int> Order => _order ??= new BaseRepoistory<Order, int>(_context);
        public IRepository<OrderDetail, int> OrderDetails => _orderDetails ??= new BaseRepoistory<OrderDetail, int>(_context);
        public IRepository<ProductImage, int> ProductImage => _productImage ??= new BaseRepoistory<ProductImage, int>(_context);
        public IRepository<UserAddress, int> UserAddress => _userAddress ??= new BaseRepoistory<UserAddress, int>(_context);
        public IRepository<Wishlist, int> Wishlist => _wishlist ??= new BaseRepoistory<Wishlist, int>(_context);
        public IRepository<WishlistItem, int> WishlistItem => _wishlistItem ??= new BaseRepoistory<WishlistItem, int>(_context);

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        // ---------------------- TRANSACTIONS ----------------------

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
