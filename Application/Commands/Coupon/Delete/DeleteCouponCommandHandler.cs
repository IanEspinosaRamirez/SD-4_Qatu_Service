using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Coupon.Delete;

internal sealed class DeleteCouponCommandHandler
    : IRequestHandler<DeleteCouponCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCouponCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteCouponCommand command,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.CouponRepository.Delete(new CustomerId(command.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
