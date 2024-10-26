using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.Update;

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        UpdateOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        var order = new Domain.Entities.Orders.Order(
            new CustomerId(command.Id),
            command.TotalPrice,
            command.ShippingMethod,
            command.PaymentMethod,
            command.UserId
        );

        await _unitOfWork.OrderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
