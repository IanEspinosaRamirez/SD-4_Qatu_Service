using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using Google.Cloud.Storage.V1;
using MediatR;

namespace Application.Commands.Photo.Create;

internal sealed class CreatePhotoCommandHandler
    : IRequestHandler<CreatePhotoCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;
  private readonly StorageClient _storageClient;
  private const string BucketName = "qatufinnisimo";

  public CreatePhotoCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    _storageClient = StorageClient.Create();
  }

  public async Task<ErrorOr<Unit>> Handle(CreatePhotoCommand command,
                                          CancellationToken cancellationToken) {
    var photoId = (Guid.NewGuid());
    var objectName = $"photos/{photoId}";

    using var fileStream = File.OpenRead(command.LocalFilePath);
    await _storageClient.UploadObjectAsync(
        BucketName, objectName, null, fileStream,
        cancellationToken: cancellationToken);

    var imageUrl = $"https://storage.googleapis.com/{BucketName}/{objectName}";

    var photo = new Domain.Entities.Photos.Photo(
        id: new CustomerId(photoId), imageURL: imageUrl,
        new CustomerId(Guid.Parse(command.ProductId!)),
        new CustomerId(Guid.Parse(command.StoreId!)));

    await _unitOfWork.PhotoRepository.Add(photo);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
