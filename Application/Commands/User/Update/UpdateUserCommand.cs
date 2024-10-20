using ErrorOr;
using MediatR;

namespace Application.Commands.User.Update;

public record UpdateUserCommand(Guid Id, string FullName, string Email,
                                string Username, string Country, string Address,
                                string? Phone = null)
    : IRequest<ErrorOr<Unit>>;
