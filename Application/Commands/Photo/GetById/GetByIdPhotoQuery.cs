using ErrorOr;
using MediatR;

namespace Application.Queries.Photo.GetPhotoUrl;

public record GetByIdPhotoQuery(Guid PhotoId) : IRequest<ErrorOr<string>>;
