using Bogus;
using Gymnastic.Domain.Models;
using Gymnastic.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Gymnastic.Persistence.Seeds
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<DataSeeder> logger)
        {
            if (!dbContext.Users.Any())
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();

                // Generate Users
                var userFaker = new Faker<ApplicationUser>()
                    .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
                    .RuleFor(u => u.UserName, (f, u) => u.Email)
                    .RuleFor(u => u.NormalizedUserName, (f, u) => u.Email.ToUpper())
                    .RuleFor(u => u.EmailConfirmed, f => true)
                    .RuleFor(u => u.SecurityStamp, f => Guid.NewGuid().ToString())
                    .RuleFor(u => u.CreatedAt, f => f.Date.Past(2))
                    .FinishWith((f, u) => u.PasswordHash = passwordHasher.HashPassword(u, "P@ssw0rd123")); // Default password

                var users = userFaker.Generate(10);
                foreach (var user in users)
                {
                    userManager.CreateAsync(user).Wait();
                }

                logger.LogInformation("✅ Seeded 10 users with passwords.");
            }
            else
            {
                logger.LogInformation("✅ Users already exist. Skipping.");
            }

            // Call other seeding methods
            SeedCategories(dbContext, logger);
            SeedProducts(dbContext, logger);
            SeedOrders(dbContext, logger);
            SeedOrderDetails(dbContext, logger);
            SeedCarts(dbContext, logger);
            SeedCartItems(dbContext, logger);
            SeedWishlists(dbContext, logger);
            SeedWishlistItems(dbContext, logger);
        }

        private static void SeedCategories(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.Categories.Any())
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

                var categories = categoryFaker.Generate(5);
                dbContext.Categories.AddRange(categories);
                dbContext.SaveChanges();

                logger.LogInformation("✅ Seeded 5 categories.");
            }
        }

        private static void SeedProducts(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.Products.Any())
            {
                var categories = dbContext.Categories.ToList();
                var productFaker = new Faker<Product>()
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(10, 500))
                    .RuleFor(p => p.Stock, f => f.Random.Int(0, 100))
                    .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id);

                var products = productFaker.Generate(20);
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();

                logger.LogInformation("✅ Seeded 20 products.");
            }
        }

        private static void SeedOrders(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.Orders.Any())
            {
                var users = dbContext.Users.ToList();
                var orderFaker = new Faker<Order>()
                    .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
                    .RuleFor(o => o.OrderDate, f => f.Date.Past(1))
                    .RuleFor(o => o.TotalAmount, f => f.Random.Decimal(50, 1000));

                var orders = orderFaker.Generate(10);
                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();

                logger.LogInformation("✅ Seeded 10 orders.");
            }
        }

        private static void SeedOrderDetails(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.OrdersDetail.Any())
            {
                var orders = dbContext.Orders.ToList();
                var products = dbContext.Products.ToList();
                var orderDetailFaker = new Faker<OrderDetail>()
                    .RuleFor(od => od.OrderId, f => f.PickRandom(orders).Id)
                    .RuleFor(od => od.ProductId, f => f.PickRandom(products).Id)
                    .RuleFor(od => od.Quantity, f => f.Random.Int(1, 5))
                    .RuleFor(od => od.UnitPrice, f => f.Random.Decimal(10, 500));

                var orderDetails = orderDetailFaker.Generate(30);
                dbContext.OrdersDetail.AddRange(orderDetails);
                dbContext.SaveChanges();

                logger.LogInformation("✅ Seeded 30 order details.");
            }
        }

        private static void SeedCarts(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.Carts.Any())
            {
                var users = dbContext.Users.ToList();
                var carts = users.Select(user => new Cart { UserId = user.Id }).ToList();

                dbContext.Carts.AddRange(carts);
                dbContext.SaveChanges();

                logger.LogInformation($"✅ Seeded {carts.Count} carts (one per user).");
            }
        }

        private static void SeedCartItems(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.CartItems.Any())
            {
                var carts = dbContext.Carts.ToList();
                var products = dbContext.Products.ToList();
                var cartItemFaker = new Faker<CartItem>()
                    .RuleFor(ci => ci.CartId, f => f.PickRandom(carts).Id)
                    .RuleFor(ci => ci.ProductId, f => f.PickRandom(products).Id)
                    .RuleFor(ci => ci.Quantity, f => f.Random.Int(1, 5));

                var cartItems = cartItemFaker.Generate(25);
                dbContext.CartItems.AddRange(cartItems);
                dbContext.SaveChanges();

                logger.LogInformation("✅ Seeded 25 cart items.");
            }
        }

        private static void SeedWishlists(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.Wishlists.Any())
            {
                var users = dbContext.Users.ToList();
                var wishlists = users.Select(user => new Wishlist { UserId = user.Id }).ToList();

                dbContext.Wishlists.AddRange(wishlists);
                dbContext.SaveChanges();

                logger.LogInformation($"✅ Seeded {wishlists.Count} wishlists (one per user).");
            }
        }

        private static void SeedWishlistItems(ApplicationDbContext dbContext, ILogger<DataSeeder> logger)
        {
            if (!dbContext.WishlistItems.Any())
            {
                var wishlists = dbContext.Wishlists.ToList();
                var products = dbContext.Products.ToList();
                var wishlistItemFaker = new Faker<WishlistItem>()
                    .RuleFor(wi => wi.WishlistId, f => f.PickRandom(wishlists).Id)
                    .RuleFor(wi => wi.ProductId, f => f.PickRandom(products).Id);

                var wishlistItems = wishlistItemFaker.Generate(20);
                dbContext.WishlistItems.AddRange(wishlistItems);
                dbContext.SaveChanges();

                logger.LogInformation("✅ Seeded 20 wishlist items.");
            }
        }
    }
}
