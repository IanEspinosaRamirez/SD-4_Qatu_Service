using Application.Queries.Photo.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.Photo.GetPagedPhotos;

internal sealed class GetPhotosPagedQueryHandler
    : IRequestHandler<GetPhotosPagedQuery,
                      ErrorOr<List<ResponseGetPagedPhotoDto>>> {

  private readonly IUnitOfWork _unitOfWork;

  public GetPhotosPagedQueryHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
  }

  public async Task<ErrorOr<List<ResponseGetPagedPhotoDto>>>
  Handle(GetPhotosPagedQuery query, CancellationToken cancellationToken) {
    var orderByExpression =
        GetOrderByExpression<Domain.Entities.Photos.Photo>(query.OrderByField);

    var photos = await _unitOfWork.PhotoRepository.GetPaged(
        query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
        orderByExpression, query.Ascending);

    if (photos == null || !photos.Any()) {
      return Error.Failure("Photo.NoRecords", "No photos found.");
    }

    var photoDtos =
        photos
            .Select(photo => new ResponseGetPagedPhotoDto {
              Id = photo.Id.Value.ToString(), ImageUrl = photo.ImageURL,
              ProductId = photo.ProductId.ToString(),
              StoreId = photo.StoreId.ToString()
            })
            .ToList();

    return photoDtos;
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
