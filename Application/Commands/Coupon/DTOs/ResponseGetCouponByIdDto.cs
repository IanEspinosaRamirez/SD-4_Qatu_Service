using Domain.Entities;
using Domain.Entities.Coupons.Enums;

namespace Application.Commands.Coupon.DTOs;

public class ResponseGetCouponByIdDto
{
    public float DiscountPercentage { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public CouponType TypeCoupon { get; set; }
    public CustomerId ProductId { get; set; }
    public CustomerId CategoryId { get; set; }
    public CustomerId StoreId { get; set; }
}
