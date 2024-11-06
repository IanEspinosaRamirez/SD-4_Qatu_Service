using Application.Queries.ReviewStore.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.ReviewStore.GetPaged;

internal sealed class GetReviewStoresPagedQueryHandler
    : IRequestHandler<GetReviewStoresPagedQuery,
                      ErrorOr<List<ResponseGetPagedReviewStoreDto>>>
{

    private readonly IUnitOfWork _unitOfWork;

    public GetReviewStoresPagedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedReviewStoreDto>>>
    Handle(GetReviewStoresPagedQuery query, CancellationToken cancellationToken)
    {
        var orderByExpression =
            GetOrderByExpression<Domain.Entities.ReviewStores.ReviewStore>(
                query.OrderByField);

        var reviewStores = await _unitOfWork.ReviewStoreRepository.GetPaged(
            query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
            orderByExpression, query.Ascending);

        if (reviewStores == null || !reviewStores.Any())
        {
            return Error.Failure("ReviewStore.NoRecords", "No review stores found.");
        }

        var reviewStoreDtos =
            reviewStores
                .Select(reviewStore => new ResponseGetPagedReviewStoreDto
                {
                    Id = reviewStore.Id.Value.ToString(),
                    rating = reviewStore.Rating,
                    content = reviewStore.Content,
                    CreateAt = reviewStore.CreatedAt,
                    UserId = reviewStore.UserId.ToString(),
                    StoreId = reviewStore.StoreId.ToString()
                })
                .ToList();

        return reviewStoreDtos;
    }

    private Expression<Func<T, object>>? GetOrderByExpression<T>(
        string? orderByField)
    {
        if (string.IsNullOrEmpty(orderByField))
        {
            return null;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, orderByField);
        var conversion = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}
