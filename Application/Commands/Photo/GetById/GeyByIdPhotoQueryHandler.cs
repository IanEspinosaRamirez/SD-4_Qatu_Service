using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using Google.Cloud.Storage.V1;
using MediatR;

namespace Application.Queries.Photo.GetPhotoUrl;

internal sealed class GetByIdPhotoQueryHandler
    : IRequestHandler<GetByIdPhotoQuery, ErrorOr<string>> {
  private readonly IUnitOfWork _unitOfWork;
  private readonly StorageClient _storageClient;
  private const string BucketName = "qatufinnisimo";

  public GetByIdPhotoQueryHandler(IUnitOfWork unitOfWork,
                                  StorageClient storageClient) {
    _unitOfWork = unitOfWork;
    _storageClient = storageClient;
  }

  public async Task<ErrorOr<string>>
  Handle(GetByIdPhotoQuery command, CancellationToken cancellationToken) {
    var photo = await _unitOfWork.PhotoRepository.GetById(
        new CustomerId(command.PhotoId));
    if (photo == null) {
      return Error.NotFound("Photo.NotFound", "Photo not found.");
    }

    var objectName = photo.ImageURL.Replace(
        $"https://storage.googleapis.com/{BucketName}/", "");

    await _storageClient.UpdateObjectAsync(
        new Google.Apis.Storage.v1.Data.Object {
          Bucket = BucketName, Name = objectName,
          Acl =
              new List<Google.Apis.Storage.v1.Data.ObjectAccessControl> {
                new Google.Apis.Storage.v1.Data.ObjectAccessControl {
                  Entity = "allUsers", Role = "READER"
                }
              }
        },
        cancellationToken: cancellationToken);

    var publicUrl = $"https://storage.googleapis.com/{BucketName}/{objectName}";
    return publicUrl;
  }
}
