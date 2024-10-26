using Application.Commands.Order.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.GetById;

internal sealed class GetByIdOrderCommandHandler
    : IRequestHandler<GetByIdOrderCommand, ErrorOr<ResponseGetOrderByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetOrderByIdDto>> Handle(
        GetByIdOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        var order = await _unitOfWork.OrderRepository.GetById(new CustomerId(command.Id));

        if (order is null)
        {
            return Error.Failure("Order.NotFound", "Order not found.");
        }

        var orderDto = new ResponseGetOrderByIdDto
        {
            TotalPrice = order.TotalPrice,
            ShippingMethod = order.ShippingMethod,
            PaymentMethod = order.PaymentMethod,
            UserId = order.UserId,
        };

        return orderDto;
    }
}
