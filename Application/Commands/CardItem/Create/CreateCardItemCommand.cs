using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.Create;

public record CreateCartItemCommand(int quantity, CustomerId cartId)
    : IRequest<ErrorOr<Unit>>;
