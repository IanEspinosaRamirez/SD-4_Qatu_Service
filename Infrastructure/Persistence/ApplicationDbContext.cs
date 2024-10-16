using Domain.Entities.Users;
using Domain.Entities.Stores;
using Domain.Entities.ReviewStores;
using Domain.Entities.ReviewProducts;
using Domain.Entities.Products;
using Domain.Entities.Photos;
using Domain.Entities.Orders;
using Domain.Entities.OrderDetails;
using Domain.Entities.Coupons;
using Domain.Entities.Categories;
using Domain.Entities.Carts;
using Domain.Entities.CartItems;

using Application.Data;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext,
                                        IApplicationDbContext,
                                        IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
            : base(options)
        {
            _publisher =
                publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        // Implementing the properties defined in IApplicationDbContext
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ReviewStore> ReviewStores { get; set; }
        public DbSet<ReviewProduct> ReviewProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; } // This was missing

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<Domain.Primitives.AggregateRoot>()
                                   .Select(e => e.Entity)
                                   .Where(e => e.GetDomainEvents().Any())
                                   .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var evt in domainEvents)
            {
                await _publisher.Publish(evt, cancellationToken);
            }

            return result;
        }
    }
}
