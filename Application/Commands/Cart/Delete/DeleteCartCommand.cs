using ErrorOr;
using MediatR;

namespace Application.Commands.Cart.Delete;

public record DeleteCartCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
