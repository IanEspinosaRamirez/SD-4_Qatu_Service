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
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Entities;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork {
  private readonly ApplicationDbContext _context;
  private IUserRepository _userRepository;
  private IStoreRepository _storeRepository;
  private IReviewStoreRepository _reviewStoreRepository;
  private IReviewProductRepository _reviewProductRepository;
  private IProductRepository _productRepository;
  private IPhotoRepository _photoRepository;
  private IOrderRepository _orderRepository;
  private IOrderDetailRepository _orderDetailRepository;
  private ICouponRepository _couponRepository;
  private ICategoryRepository _categoryRepository;
  private ICartRepository _cartRepository;
  private ICartItemRepository _cartItemRepository;

  public UnitOfWork(ApplicationDbContext context) { _context = context; }

  public IUserRepository UserRepository => _userRepository ??=
      new UserRepository(_context);

  public IStoreRepository StoreRepository => _storeRepository ??=
      new StoreRepository(_context);

  public IReviewStoreRepository ReviewStoreRepository =>
      _reviewStoreRepository ??= new ReviewStoreRepository(_context);

  public IReviewProductRepository ReviewProductRepository =>
      _reviewProductRepository ??= new ReviewProductRepository(_context);

  public IProductRepository ProductRepository => _productRepository ??=
      new ProductRepository(_context);

  public IPhotoRepository PhotoRepository => _photoRepository ??=
      new PhotoRepository(_context);

  public IOrderRepository OrderRepository => _orderRepository ??=
      new OrderRepository(_context);

  public IOrderDetailRepository OrderDetailRepository =>
      _orderDetailRepository ??= new OrderDetailRepository(_context);

  public ICouponRepository CouponRepository => _couponRepository ??=
      new CouponRepository(_context);

  public ICategoryRepository CategoryRepository => _categoryRepository ??=
      new CategoryRepository(_context);

  public ICartRepository CartRepository => _cartRepository ??=
      new CartRepository(_context);

  public ICartItemRepository CartItemRepository => _cartItemRepository ??=
      new CartItemRepository(_context);

  public async Task BeginTransactionAsync() {
    await _context.BeginTransactionAsync();
  }

  public async Task CommitTransactionAsync() {
    await _context.CommitTransactionAsync();
  }

  public async Task RollbackTransactionAsync() {
    await _context.RollbackTransactionAsync();
  }

  public async Task<int>
  SaveChangesAsync(CancellationToken cancellationToken = default) {
    return await _context.SaveChangesAsync(cancellationToken);
  }

  public void Dispose() { _context.Dispose(); }
}
