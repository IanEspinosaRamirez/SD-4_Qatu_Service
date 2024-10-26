using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.Update;

public record UpdateOrderCommand(
    Guid Id,
    float TotalPrice,
    string ShippingMethod,
    string PaymentMethod,
    CustomerId UserId
) : IRequest<ErrorOr<Unit>>;
