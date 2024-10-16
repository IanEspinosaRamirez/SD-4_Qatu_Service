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

using Domain.Primitives;
using Application.Data;

using Infrastructure.Persistence.Repositories.Entities;

using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {
  public static IServiceCollection
  AddInfrastructure(this IServiceCollection services,
                    IConfiguration configuration) {
    services.AddPersistence(configuration);
    return services;
  }

  private static IServiceCollection
  AddPersistence(this IServiceCollection services,
                 IConfiguration configuration) {
    var connectionString =
        configuration.GetConnectionString("DefaultConnection");

    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

    services.AddDbContext<ApplicationDbContext>(
        options => options.UseMySql(connectionString, serverVersion));

    services.AddScoped<IApplicationDbContext>(
        sp => sp.GetRequiredService<ApplicationDbContext>());
    services.AddScoped<IUnitOfWork>(
        sp => sp.GetRequiredService<ApplicationDbContext>());

    // Repositorios
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IStoreRepository, StoreRepository>();
    services.AddScoped<IReviewStoreRepository, ReviewStoreRepository>();
    services.AddScoped<IReviewProductRepository, ReviewProductRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IPhotoRepository, PhotoRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
    services.AddScoped<ICouponRepository, CouponRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<ICartItemRepository, CartItemRepository>();

    return services;
  }
}
