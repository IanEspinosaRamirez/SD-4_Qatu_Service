using ErrorOr;
using MediatR;
using Domain.Entities;
using Domain.Primitives;
using Domain.Entities.Users.Enums;

namespace Application.Commands.User.UpdateRole;

internal sealed class UpdateRoleUserCommandHandler
    : IRequestHandler<UpdateRoleUserCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public UpdateRoleUserCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(UpdateRoleUserCommand command,
                                          CancellationToken cancellationToken) {

    if (!Enum.TryParse(command.NewRole, ignoreCase: true, out UserRole role)) {
      return Error.Validation("InvalidRole", "The specified role is invalid.");
    }

    var user = new Domain.Entities.Users.User(
        new CustomerId(Guid.Parse(command.UserId)), "", "", "", "", "", "",
        null, null);

    user.RoleUser = role;
    user.UpdatedAt = DateTime.Now;

    await _unitOfWork.UserRepository.UpdatePartial(user, nameof(user.RoleUser),
                                                   nameof(user.UpdatedAt));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
