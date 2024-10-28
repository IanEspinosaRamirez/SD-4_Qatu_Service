using Application.Commands.Coupon.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.GetPaged;

internal sealed class GetCouponsPagedQueryHandler
    : IRequestHandler<GetCouponsPagedQuery, ErrorOr<List<ResponseGetPagedCouponDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCouponsPagedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedCouponDto>>> Handle(
        GetCouponsPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        var coupons = await _unitOfWork.CouponRepository.GetPaged(
            request.PageNumber,
            request.PageSize
        );

        if (!coupons.Any())
        {
            return Error.Failure("Coupon.NoRecords", "No coupons found.");
        }

        var couponDtos = coupons
            .Select(coupon => new ResponseGetPagedCouponDto
            {
                DiscountPercentage = coupon.DiscountPercentage,
                ExpirationDate = coupon.ExpirationDate,
                IsActive = coupon.IsActive,
                TypeCoupon = coupon.TypeCoupon,
                ProductId = coupon.ProductId,
                CategoryId = coupon.CategoryId,
                StoreId = coupon.StoreId,
            })
            .ToList();

        return couponDtos;
    }
}
