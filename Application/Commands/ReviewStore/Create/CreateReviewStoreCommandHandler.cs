using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStores.Create;

internal sealed class CreateReviewStoreCommandHandler
    : IRequestHandler<CreateReviewStoreCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public CreateReviewStoreCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(CreateReviewStoreCommand command,
                                          CancellationToken cancellationToken) {

    var reviewStore = new Domain.Entities.ReviewStores.ReviewStore(
        new CustomerId(Guid.NewGuid()), command.rating, command.content,
        command.userId, command.storeId);

    await _unitOfWork.ReviewStoreRepository.Add(reviewStore);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
