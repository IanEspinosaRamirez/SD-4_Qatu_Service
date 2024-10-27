using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.Update;

internal sealed class UpdateCouponCommandHandler
    : IRequestHandler<UpdateCouponCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCouponCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        UpdateCouponCommand command,
        CancellationToken cancellationToken
    )
    {
        var coupon = new Domain.Entities.Coupons.Coupon(
            new CustomerId(command.Id),
            command.DiscountPercentage,
            command.ExpirationDate,
            command.IsActive,
            command.TypeCoupon,
            command.ProductId,
            command.CategoryId,
            command.StoreId
        );

        await _unitOfWork.CouponRepository.Update(coupon);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
