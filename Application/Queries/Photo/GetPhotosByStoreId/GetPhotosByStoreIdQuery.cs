using Application.Queries.Photo.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Photo.GetPhotosByStoreId;

public record GetPhotosByStoreIdQuery(string StoreId)
    : IRequest<ErrorOr<List<ResponseGetPhotoDto>>>;
