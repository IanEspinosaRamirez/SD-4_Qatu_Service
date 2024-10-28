using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.Create;

internal sealed class CreateOrderDetailCommandHandler
    : IRequestHandler<CreateOrderDetailCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(CreateOrderDetailCommand command,
                                            CancellationToken cancellationToken)
    {
        var orderDetail = new Domain.Entities.OrderDetails.OrderDetail(
            new CustomerId(Guid.NewGuid()), command.Quantity, command.UnitPrice,
            command.orderId, command.productId);

        await _unitOfWork.OrderDetailRepository.Add(orderDetail);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
