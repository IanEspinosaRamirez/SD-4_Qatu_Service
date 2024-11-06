using Application.Querys.ReviewProduct.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System.Linq.Expressions;

namespace Application.Querys.ReviewProduct.GetPaged;

internal sealed class GetReviewProductsPagedQueryHandler
    : IRequestHandler<GetReviewProductsPagedQuery,
                      ErrorOr<List<ResponseGetPagedReviewProductDto>>> {

  private readonly IUnitOfWork _unitOfWork;

  public GetReviewProductsPagedQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetPagedReviewProductDto>>>
  Handle(GetReviewProductsPagedQuery query,
         CancellationToken cancellationToken) {
    var orderByExpression =
        GetOrderByExpression<Domain.Entities.ReviewProducts.ReviewProduct>(
            query.OrderByField);

    var reviewProducts = await _unitOfWork.ReviewProductRepository.GetPaged(
        query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
        orderByExpression, query.Ascending);

    if (reviewProducts == null || !reviewProducts.Any()) {
      return Error.Failure("ReviewProduct.NoRecords",
                           "No review products found.");
    }

    var reviewProductDtos =
        reviewProducts
            .Select(reviewProduct => new ResponseGetPagedReviewProductDto {
              Id = reviewProduct.Id.Value.ToString(),
              rating = reviewProduct.Rating, content = reviewProduct.Content,
              CreateAt = reviewProduct.CreatedAt,
              UserId = reviewProduct.UserId.ToString(),
              ProductId = reviewProduct.ProductId.ToString()
            })
            .ToList();

    return reviewProductDtos;
  }

  private Expression<Func<T, object>>? GetOrderByExpression<T>(
      string? orderByField) {
    if (string.IsNullOrEmpty(orderByField)) {
      return null;
    }

    var parameter = Expression.Parameter(typeof(T), "x");
    var property = Expression.PropertyOrField(parameter, orderByField);
    var conversion = Expression.Convert(property, typeof(object));
    return Expression.Lambda<Func<T, object>>(conversion, parameter);
  }
}
