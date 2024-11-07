using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using Google.Cloud.Storage.V1;
using MediatR;

namespace Application.Commands.User.UpdatePhoto;

internal sealed class UpdatePhotoUserCommandHandler
    : IRequestHandler<UpdatePhotoUserCommand, ErrorOr<Unit>> {
  private readonly IUnitOfWork _unitOfWork;
  private readonly StorageClient _storageClient;
  private const string BucketName = "qatufinnisimo";

  public UpdatePhotoUserCommandHandler(IUnitOfWork unitOfWork) {
    _unitOfWork =
        unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    _storageClient = StorageClient.Create();
  }

  public async Task<ErrorOr<Unit>> Handle(UpdatePhotoUserCommand command,
                                          CancellationToken cancellationToken) {
    var userId = new CustomerId(Guid.Parse(command.UserId));
    var objectName = $"photos/{command.UserId}";

    using var fileStream = File.OpenRead(command.LocalFilePath);
    await _storageClient.UploadObjectAsync(
        BucketName, objectName, null, fileStream,
        cancellationToken: cancellationToken);

    var imageUrl = $"https://storage.googleapis.com/{BucketName}/{objectName}";

    var user =
        new Domain.Entities.Users.User(userId, "", "", "", "", "", "", null);
    user.ImageURL = imageUrl;
    user.UpdatedAt = DateTime.Now;

    await _unitOfWork.UserRepository.UpdatePartial(user, nameof(user.ImageURL),
                                                   nameof(user.UpdatedAt));
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
