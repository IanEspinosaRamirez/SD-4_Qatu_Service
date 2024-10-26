using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.Create;

public record CreateOrderCommand(
    float TotalPrice,
    string ShippingMethod,
    string PaymentMethod,
    CustomerId UserId
) : IRequest<ErrorOr<Unit>>;
