using Application.Commands.ReviewStore.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStore.GetById;

internal sealed class GetByIdReviewStoreCommandHandler
    : IRequestHandler<GetByIdReviewStoreCommand,
                      ErrorOr<ResponseGetReviewStoreByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdReviewStoreCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetReviewStoreByIdDto>>
  Handle(GetByIdReviewStoreCommand command,
         CancellationToken cancellationToken) {

    var reviewStore = await _unitOfWork.ReviewStoreRepository.GetById(
        new CustomerId(command.Id));

    if (reviewStore is null) {

      return Error.Failure("ReviewStore.NotFound", "Review Store not found.");
    }

    var reviewStoreDto =
        new ResponseGetReviewStoreByIdDto { Rating = reviewStore.Rating,
                                            Content = reviewStore.Content,
                                            CreateAt = reviewStore.CreatedAt };

    return reviewStoreDto;
  }
}
