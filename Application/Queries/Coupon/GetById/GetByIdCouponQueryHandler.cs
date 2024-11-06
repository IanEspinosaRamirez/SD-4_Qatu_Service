using Application.Queries.Coupon.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Coupon.GetById;

internal sealed class GetByIdCouponQueryHandler
    : IRequestHandler<GetByIdCouponQuery, ErrorOr<ResponseGetCouponByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdCouponQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetCouponByIdDto>>
  Handle(GetByIdCouponQuery query, CancellationToken cancellationToken) {
    var coupon =
        await _unitOfWork.CouponRepository.GetById(new CustomerId(query.Id));

    if (coupon is null) {
      return Error.Failure("Coupon.NotFound", "Coupon not found.");
    }

    var couponDto = new ResponseGetCouponByIdDto {
      DiscountPercentage = coupon.DiscountPercentage,
      ExpirationDate = coupon.ExpirationDate,
      IsActive = coupon.IsActive,
      TypeCoupon = coupon.TypeCoupon,
      ProductId = coupon.ProductId,
      CategoryId = coupon.CategoryId,
      StoreId = coupon.StoreId,
    };

    return couponDto;
  }
}
