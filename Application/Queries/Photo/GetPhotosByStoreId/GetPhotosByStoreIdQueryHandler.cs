using Application.Queries.Photo.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Photo.GetPhotosByStoreId;

internal sealed class GetPhotosByStoreIdQueryHandler
    : IRequestHandler<GetPhotosByStoreIdQuery,
                      ErrorOr<List<ResponseGetPhotoDto>>>
{

    private readonly IUnitOfWork _unitOfWork;

    public GetPhotosByStoreIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPhotoDto>>>
    Handle(GetPhotosByStoreIdQuery query, CancellationToken cancellationToken)
    {
        var photos =
            await _unitOfWork.PhotoRepository.GetPhotosByStoreId(query.StoreId);

        if (photos == null || !photos.Any())
        {
            return Error.Failure("Photo.NoRecords",
                                 "No photos found for the specified Store ID.");
        }

        var photoDtos =
            photos
                .Select(photo => new ResponseGetPhotoDto
                {
                    Id = photo.Id.Value.ToString(),
                    ImageUrl = photo.ImageURL,
                    ProductId = photo.ProductId.ToString(),
                    StoreId = photo.StoreId.ToString()
                })
                .ToList();

        return photoDtos;
    }
}
