using Application.Commands.ReviewProduct.Delete;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStores.Delete;

internal sealed class DeleteReviewProductCommandHandler
    : IRequestHandler<DeleteReviewProductCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public DeleteReviewProductCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(DeleteReviewProductCommand command,
                                          CancellationToken cancellationToken) {

    await _unitOfWork.ReviewProductRepository.Delete(new CustomerId(command.Id));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }

}
