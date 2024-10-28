using ErrorOr;
using MediatR;

namespace Application.Commands.Photo.Delete;

public record DeletePhotoCommand(Guid PhotoId) : IRequest<ErrorOr<Unit>>;
