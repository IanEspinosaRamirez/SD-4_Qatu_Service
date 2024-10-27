using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.Delete;

public record DeleteCartItemCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
