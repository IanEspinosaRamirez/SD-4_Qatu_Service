using ErrorOr;
using MediatR;

namespace Application.Commands.Photo.Create;

public record CreatePhotoCommand(string LocalFilePath, string? ProductId = null,
                                 string? StoreId = null)
    : IRequest<ErrorOr<Unit>>;
