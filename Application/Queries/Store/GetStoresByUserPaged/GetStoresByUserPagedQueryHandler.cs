using Application.Queries.Store.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.Store.GetStoresByUserPaged;

internal sealed class GetStoresByUserPagedQueryHandler
    : IRequestHandler<GetStoresByUserPagedQuery,
                      ErrorOr<List<ResponseGetPagedStoreDto>>> {
  private readonly IUnitOfWork _unitOfWork;

  public GetStoresByUserPagedQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetPagedStoreDto>>>
  Handle(GetStoresByUserPagedQuery query, CancellationToken cancellationToken) {
    var orderByExpression =
        GetOrderByExpression<Domain.Entities.Stores.Store>(query.OrderByField);

    var stores = await _unitOfWork.StoreRepository.GetStoresByUserPaged(
        query.UserId, query.PageNumber, query.PageSize, query.FilterField,
        query.FilterValue, orderByExpression, query.Ascending);

    if (stores == null || !stores.Any()) {
      return Error.Failure("Store.NoRecords", "No stores found.");
    }

    var storeDtos =
        stores
            .Select(store => new ResponseGetPagedStoreDto {
              Id = store.Id.Value.ToString(), Name = store.Name,
              Description = store.Description, Address = store.Address,
              UserId = store.UserId.Value.ToString(),
              CreatedAt = store.CreatedAt, UpdatedAt = store.UpdatedAt
            })
            .ToList();

    return storeDtos;
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
