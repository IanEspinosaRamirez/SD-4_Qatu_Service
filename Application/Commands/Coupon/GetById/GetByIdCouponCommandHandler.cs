using Application.Commands.Coupon.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.GetById;

internal sealed class GetByIdCouponCommandHandler
    : IRequestHandler<GetByIdCouponCommand, ErrorOr<ResponseGetCouponByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdCouponCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetCouponByIdDto>> Handle(
        GetByIdCouponCommand command,
        CancellationToken cancellationToken
    )
    {
        var coupon = await _unitOfWork.CouponRepository.GetById(new CustomerId(command.Id));

        if (coupon is null)
        {
            return Error.Failure("Coupon.NotFound", "Coupon not found.");
        }

        var couponDto = new ResponseGetCouponByIdDto
        {
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
