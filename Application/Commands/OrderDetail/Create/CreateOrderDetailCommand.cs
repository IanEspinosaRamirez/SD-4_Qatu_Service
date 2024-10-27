using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.OrderDetail.Create;

public record CreateOrderDetailCommand(int Quantity, float UnitPrice,
                                       CustomerId orderId, CustomerId productId)
    : IRequest<ErrorOr<Unit>>;
