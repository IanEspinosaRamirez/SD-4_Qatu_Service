using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.Update;

public record UpdateCartItemCommand(Guid Id, int Quantity)
    : IRequest<ErrorOr<Unit>>;
