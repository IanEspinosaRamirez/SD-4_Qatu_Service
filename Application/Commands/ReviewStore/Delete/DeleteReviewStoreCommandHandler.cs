using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStores.Delete;

internal sealed class DeleteReviewStoreCommandHandler
    : IRequestHandler<DeleteReviewStoreCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public DeleteReviewStoreCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(DeleteReviewStoreCommand command,
                                          CancellationToken cancellationToken) {

    await _unitOfWork.ReviewStoreRepository.Delete(new CustomerId(command.Id));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
