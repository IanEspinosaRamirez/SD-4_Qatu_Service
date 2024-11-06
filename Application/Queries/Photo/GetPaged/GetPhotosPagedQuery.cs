using Application.Queries.Photo.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Photo.GetPagedPhotos;

public record
GetPhotosPagedQuery(int PageNumber, int PageSize, string? FilterField = null,
                    string? FilterValue = null, string? OrderByField = null,
                    bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedPhotoDto>>>;
