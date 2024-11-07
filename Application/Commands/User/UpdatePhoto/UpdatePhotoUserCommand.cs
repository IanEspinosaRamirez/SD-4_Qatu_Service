using ErrorOr;
using MediatR;

namespace Application.Commands.User.UpdatePhoto;

public record UpdatePhotoUserCommand(string UserId, string LocalFilePath)
    : IRequest<ErrorOr<Unit>>;
