using ErrorOr;
using MediatR;
using Domain.Entities.Users.Enums;

namespace Application.Commands.User.UpdateRole;

public record UpdateRoleUserCommand(string UserId, string NewRole)
    : IRequest<ErrorOr<Unit>>;
