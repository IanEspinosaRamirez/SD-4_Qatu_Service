using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStore.Update;

internal sealed class UpdateReviewStoreCommandHandler
    : IRequestHandler<UpdateReviewStoreCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public UpdateReviewStoreCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(UpdateReviewStoreCommand command,
                                          CancellationToken cancellationToken) {

    var reviewStore = new Domain.Entities.ReviewStores.ReviewStore(
        new CustomerId(command.Id), command.Rating, command.Content, null,
        null);

    await _unitOfWork.ReviewStoreRepository.UpdatePartial(
        reviewStore, nameof(reviewStore.Rating), nameof(reviewStore.Content));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
