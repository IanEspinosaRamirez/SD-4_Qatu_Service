using Domain.Entities.Coupons.Enums;
using Domain.Primitives;

namespace Domain.Entities.Coupons;

public class Coupon : AggregateRoot {
  public CustomerId Id { get; set; }

  public float DiscountPercentage { get; set; }

  public DateTime ExpirationDate { get; set; }

  public Boolean IsActive { get; set; }

  public CouponType TypeCoupon { get; set; }
}
