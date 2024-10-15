using Domain.Entities.Coupons.Enums;
using Domain.Entities.Products;
using Domain.Entities.Categories;
using Domain.Entities.Stores;
using Domain.Primitives;

namespace Domain.Entities.Coupons;

public class Coupon : AggregateRoot {
  public Coupon(CustomerId id, float discountPercentage,
                DateTime expirationDate, bool isActive, CouponType typeCoupon,
                CustomerId? productId = null, Product? product = null,
                CustomerId? categoryId = null, Category? category = null,
                CustomerId? storeId = null, Store? store = null) {
    Id = id;
    DiscountPercentage = discountPercentage;
    ExpirationDate = expirationDate;
    IsActive = isActive;
    TypeCoupon = typeCoupon;

    // Setting relations based on TypeCoupon
    if (typeCoupon == CouponType.Product && productId != null &&
        product != null) {
      ProductId = productId;
      Product = product;
    } else if (typeCoupon == CouponType.Category && categoryId != null &&
               category != null) {
      CategoryId = categoryId;
      Category = category;
    } else if (typeCoupon == CouponType.Store && storeId != null &&
               store != null) {
      StoreId = storeId;
      Store = store;
    } else {
      throw new ArgumentException(
          "Invalid coupon type or missing related entity");
    }
  }

  public CustomerId Id { get; set; }

  public float DiscountPercentage { get; set; }

  public DateTime ExpirationDate { get; set; }

  public Boolean IsActive { get; set; }

  public CouponType TypeCoupon { get; set; }

  // Optional foreign key for Product (Only applicable if TypeCoupon == Product)
  public CustomerId? ProductId { get; set; }

  // Navigation property for Product
  public Product? Product { get; set; }

  // Optional foreign key for Category (Only applicable if TypeCoupon ==
  // Category)
  public CustomerId? CategoryId { get; set; }

  // Navigation property for Category
  public Category? Category { get; set; }

  // Optional foreign key for Store (Only applicable if TypeCoupon == Store)
  public CustomerId? StoreId { get; set; }

  // Navigation property for Store
  public Store? Store { get; set; }
}
