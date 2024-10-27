using Application.Commands.ReviewProduct.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.GetById;

internal sealed class GetByIdReviewProductCommandHandler
    : IRequestHandler<GetByIdReviewProductCommand,
                      ErrorOr<ResponseGetReviewProductByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdReviewProductCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetReviewProductByIdDto>>
  Handle(GetByIdReviewProductCommand command,
         CancellationToken cancellationToken) {

    var reviewPReviewProduct = await _unitOfWork.ReviewProductRepository.GetById(
        new CustomerId(command.Id));

    if (reviewPReviewProduct is null) {

      return Error.Failure("ReviewProduct.NotFound", "Review Store not found.");
    }

    var reviewPReviewProductDto =
        new ResponseGetReviewProductByIdDto { Rating = reviewPReviewProduct.Rating,
                                            Content = reviewPReviewProduct.Content,
                                            CreateAt = reviewPReviewProduct.CreatedAt };

    return reviewPReviewProductDto;
  }
}
