using Application.Queries.Photo.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Photo.GetPhotosByProductId;

public record GetPhotosByProductIdQuery(string ProductId)
    : IRequest<ErrorOr<List<ResponseGetPhotoDto>>>;
