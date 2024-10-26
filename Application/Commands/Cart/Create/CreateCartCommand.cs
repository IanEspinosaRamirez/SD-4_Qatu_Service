using ErrorOr;
using MediatR;

namespace Application.Commands.Cart.Create;

public record CreateCartCommand(Guid UserId) : IRequest<ErrorOr<Unit>>;
