using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.Photo.Create;

public record CreatePhotoCommand(string LocalFilePath,
                                 CustomerId? ProductId = null,
                                 CustomerId? StoreId = null)
    : IRequest<ErrorOr<Unit>>;
