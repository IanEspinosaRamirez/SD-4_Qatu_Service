using Application.Queries.ReviewStore.DTOs;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.ReviewStore.GetById;

internal sealed class GetByIdReviewStoreQueryHandler
    : IRequestHandler<GetByIdReviewStoreQuery,
                      ErrorOr<ResponseGetReviewStoreByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdReviewStoreQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetReviewStoreByIdDto>>
    Handle(GetByIdReviewStoreQuery query, CancellationToken cancellationToken)
    {

        var reviewStore = await _unitOfWork.ReviewStoreRepository.GetById(
            new CustomerId(query.Id));

        if (reviewStore is null)
        {

            return Error.Failure("ReviewStore.NotFound", "Review Store not found.");
        }

        var reviewStoreDto =
            new ResponseGetReviewStoreByIdDto
            {
                Rating = reviewStore.Rating,
                Content = reviewStore.Content,
                CreateAt = reviewStore.CreatedAt
            };

        return reviewStoreDto;
    }
}
