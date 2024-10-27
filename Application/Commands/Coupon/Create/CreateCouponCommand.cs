using Domain.Entities;
using Domain.Entities.Coupons.Enums;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.Create;

public record CreateCouponCommand(
    float DiscountPercentage,
    DateTime ExpirationDate,
    bool IsActive,
    CouponType TypeCoupon,
    CustomerId ProductId,
    CustomerId CategoryId,
    CustomerId StoreId
) : IRequest<ErrorOr<Unit>>;
