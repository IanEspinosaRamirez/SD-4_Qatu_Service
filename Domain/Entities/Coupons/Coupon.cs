using Domain.Entities.Coupons.Enums;
using Domain.Entities.Products;
using Domain.Entities.Categories;
using Domain.Entities.Stores;
using Domain.Primitives;

namespace Domain.Entities.Coupons;

public class Coupon : AggregateRoot
{
    public Coupon() { }

    public Coupon(CustomerId id, float discountPercentage,
                  DateTime expirationDate, bool isActive, CouponType typeCoupon,
                  CustomerId? productId = null, CustomerId? categoryId = null,
                  CustomerId? storeId = null)
    {
        Id = id;
        DiscountPercentage = discountPercentage;
        ExpirationDate = expirationDate;
        IsActive = isActive;
        TypeCoupon = typeCoupon;
        ProductId = productId;
        CategoryId = categoryId;
        StoreId = storeId;
    }

    public CustomerId Id { get; set; }
    public float DiscountPercentage { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public CouponType TypeCoupon { get; set; }

    // Claves foráneas opcionales
    public CustomerId? ProductId { get; set; }
    public CustomerId? CategoryId { get; set; }
    public CustomerId? StoreId { get; set; }

    // Propiedades de navegación
    public Product? Product { get; set; }
    public Category? Category { get; set; }
    public Store? Store { get; set; }
}
