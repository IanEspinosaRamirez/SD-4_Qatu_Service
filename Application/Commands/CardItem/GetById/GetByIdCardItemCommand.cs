using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.GetById;

public record GetByIdCartItemCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
