using ErrorOr;
using MediatR;

namespace Application.Commands.Cart.Update;

public record UpdateCartCommand(Guid UserId) : IRequest<ErrorOr<Unit>>;
