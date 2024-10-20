using Domain.Entities.CartItems;
using Domain.Entities.Carts;
using Domain.Entities.Categories;
using Domain.Entities.Coupons;
using Domain.Entities.OrderDetails;
using Domain.Entities.Orders;
using Domain.Entities.Photos;
using Domain.Entities.Products;
using Domain.Entities.ReviewProducts;
using Domain.Entities.ReviewStores;
using Domain.Entities.Stores;
using Domain.Entities.Users;

namespace Domain.Primitives;

public interface IUnitOfWork : IDisposable {
  IUserRepository UserRepository { get; }
  IStoreRepository StoreRepository { get; }
  IReviewStoreRepository ReviewStoreRepository { get; }
  IReviewProductRepository ReviewProductRepository { get; }
  IProductRepository ProductRepository { get; }
  IPhotoRepository PhotoRepository { get; }
  IOrderRepository OrderRepository { get; }
  IOrderDetailRepository OrderDetailRepository { get; }
  ICouponRepository CouponRepository { get; }
  ICategoryRepository CategoryRepository { get; }
  ICartRepository CartRepository { get; }
  ICartItemRepository CartItemRepository { get; }

  Task BeginTransactionAsync();
  Task CommitTransactionAsync();
  Task RollbackTransactionAsync();
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
