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

using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext {
  DbSet<User> Users { get; set; }
  DbSet<Store> Stores { get; set; }
  DbSet<ReviewStore> ReviewStores { get; set; }
  DbSet<ReviewProduct> ReviewProducts { get; set; }
  DbSet<Product> Products { get; set; }
  DbSet<Photo> Photos { get; set; }
  DbSet<Order> Orders { get; set; }
  DbSet<OrderDetail> OrderDetails { get; set; }
  DbSet<Coupon> Coupons { get; set; }
  DbSet<Category> Categories { get; set; }
  DbSet<Cart> Carts { get; set; }
  DbSet<CartItem> CartItems { get; set; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
