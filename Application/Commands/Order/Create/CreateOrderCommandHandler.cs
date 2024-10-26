using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        var order = new Domain.Entities.Orders.Order(
            new CustomerId(Guid.NewGuid()),
            command.TotalPrice,
            command.ShippingMethod,
            command.PaymentMethod,
            command.UserId
        );

        await _unitOfWork.OrderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
