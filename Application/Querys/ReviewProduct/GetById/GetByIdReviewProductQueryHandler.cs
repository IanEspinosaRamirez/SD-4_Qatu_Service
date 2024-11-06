using Application.Querys.ReviewProduct.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Querys.ReviewProduct.GetById;

internal sealed class GetByIdReviewProductQueryHandler
    : IRequestHandler<GetByIdReviewProductQuery,
                      ErrorOr<ResponseGetReviewProductByIdDto>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetByIdReviewProductQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<ResponseGetReviewProductByIdDto>>
  Handle(GetByIdReviewProductQuery query, CancellationToken cancellationToken) {

    var reviewPReviewProduct =
        await _unitOfWork.ReviewProductRepository.GetById(
            new CustomerId(query.Id));

    if (reviewPReviewProduct is null) {

      return Error.Failure("ReviewProduct.NotFound", "Review Store not found.");
    }

    var reviewPReviewProductDto = new ResponseGetReviewProductByIdDto {
      Rating = reviewPReviewProduct.Rating,
      Content = reviewPReviewProduct.Content,
      CreateAt = reviewPReviewProduct.CreatedAt
    };

    return reviewPReviewProductDto;
  }
}
