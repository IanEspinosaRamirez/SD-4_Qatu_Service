using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.Create;

internal sealed class CreateCouponCommandHandler
    : IRequestHandler<CreateCouponCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCouponCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        CreateCouponCommand command,
        CancellationToken cancellationToken
    )
    {
        var coupon = new Domain.Entities.Coupons.Coupon(
            new CustomerId(Guid.NewGuid()),
            command.DiscountPercentage,
            command.ExpirationDate,
            command.IsActive,
            command.TypeCoupon,
            command.ProductId,
            command.CategoryId,
            command.StoreId
        );

        await _unitOfWork.CouponRepository.Add(coupon);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
