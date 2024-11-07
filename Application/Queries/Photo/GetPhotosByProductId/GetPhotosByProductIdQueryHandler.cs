using Application.Queries.Photo.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Photo.GetPhotosByProductId;

internal sealed class GetPhotosByProductIdQueryHandler
    : IRequestHandler<GetPhotosByProductIdQuery,
                      ErrorOr<List<ResponseGetPhotoDto>>>
{

    private readonly IUnitOfWork _unitOfWork;

    public GetPhotosByProductIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPhotoDto>>>
    Handle(GetPhotosByProductIdQuery query, CancellationToken cancellationToken)
    {
        var photos =
            await _unitOfWork.PhotoRepository.GetPhotosByProductId(query.ProductId);

        if (photos == null || !photos.Any())
        {
            return Error.Failure("Photo.NoRecords",
                                 "No photos found for the specified Product ID.");
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
