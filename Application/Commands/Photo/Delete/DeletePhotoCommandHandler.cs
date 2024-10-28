using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using Google;
using Google.Cloud.Storage.V1;
using MediatR;

namespace Application.Commands.Photo.Delete;

internal sealed class DeletePhotoCommandHandler
    : IRequestHandler<DeletePhotoCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly StorageClient _storageClient;
    private const string BucketName = "finnisimoqatu";

    public DeletePhotoCommandHandler(IUnitOfWork unitOfWork,
                                     StorageClient storageClient)
    {
        _unitOfWork = unitOfWork;
        _storageClient = storageClient;
    }

    public async Task<ErrorOr<Unit>> Handle(DeletePhotoCommand command,
                                            CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(command.PhotoId);

        try
        {
            var objectName = $"photos/{customerId}";
            await _storageClient.DeleteObjectAsync(
                BucketName, objectName, cancellationToken: cancellationToken);
        }
        catch (GoogleApiException ex) when (ex.Error.Code == 404)
        {
        }

        await _unitOfWork.PhotoRepository.Delete(customerId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
