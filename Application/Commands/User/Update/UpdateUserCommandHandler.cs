using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.Update;

internal sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public UpdateUserCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand command,
                                          CancellationToken cancellationToken) {
    var user = new Domain.Entities.Users.User(
        new CustomerId(command.Id), command.FullName, command.Email,
        command.Username, "", command.Country, command.Address, command.Phone,
        null);

    user.UpdatedAt = DateTime.Now;

    await _unitOfWork.UserRepository.UpdatePartial(
        user, nameof(user.FullName), nameof(user.Email), nameof(user.Username),
        nameof(user.Country), nameof(user.Address), nameof(user.Phone),
        nameof(user.UpdatedAt));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
