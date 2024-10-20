using ErrorOr;
using MediatR;

namespace Application.Commands.User.Delete;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
