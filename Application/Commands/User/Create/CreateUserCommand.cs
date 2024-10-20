using ErrorOr;
using MediatR;

namespace Application.Commands.User.Create;

public record CreateUserCommand(string FullName, string Email, string Username,
                                string Password, string Country, string Address,
                                string? Phone = null, string? ImageURL = null)
    : IRequest<ErrorOr<Unit>>;
