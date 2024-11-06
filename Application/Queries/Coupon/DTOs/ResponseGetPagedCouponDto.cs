using Domain.Entities;
using Domain.Entities.Coupons.Enums;

namespace Application.Queries.Coupon.DTOs;

public class ResponseGetPagedCouponDto {
  public required float DiscountPercentage { get; set; }
  public required DateTime ExpirationDate { get; set; }
  public required bool IsActive { get; set; }
  public required CouponType TypeCoupon { get; set; }
  public required CustomerId ProductId { get; set; }
  public required CustomerId CategoryId { get; set; }
  public required CustomerId StoreId { get; set; }
}
