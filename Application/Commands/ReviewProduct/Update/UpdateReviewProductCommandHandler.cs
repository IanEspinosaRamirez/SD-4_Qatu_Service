using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.Update;

internal sealed class UpdateReviewProductCommandHandler
    : IRequestHandler<UpdateReviewProductCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;

  public UpdateReviewProductCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<Unit>> Handle(UpdateReviewProductCommand command,
                                          CancellationToken cancellationToken) {

    var reviewProduct = new Domain.Entities.ReviewProducts.ReviewProduct(
        new CustomerId(command.Id), command.Rating, command.Content, null,
        null);

    await _unitOfWork.ReviewProductRepository.UpdatePartial(
        reviewProduct, nameof(reviewProduct.Rating), nameof(reviewProduct.Content));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
