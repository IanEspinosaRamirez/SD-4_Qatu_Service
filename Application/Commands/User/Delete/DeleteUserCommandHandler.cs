using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.Delete;

internal sealed class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public DeleteUserCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command,
                                          CancellationToken cancellationToken) {

    await _unitOfWork.UserRepository.Delete(command.Id);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
