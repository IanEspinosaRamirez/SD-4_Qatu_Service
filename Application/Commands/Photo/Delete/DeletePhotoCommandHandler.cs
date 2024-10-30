using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using Google;
using Google.Cloud.Storage.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Photo.Delete;

internal sealed class DeletePhotoCommandHandler
    : IRequestHandler<DeletePhotoCommand, ErrorOr<Unit>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly StorageClient _storageClient;
    private const string BucketName = "qatufinnisimo";

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
        catch (GoogleApiException ex)
        {
            Console.WriteLine($"Error eliminando el objeto en GCS: {ex.Message}");
            return Error.Failure("Photo.StorageError",
                                 "Failed to delete photo in storage.");
        }

        try
        {
            await _unitOfWork.PhotoRepository.Delete(customerId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Error.Conflict("Photo.ConcurrencyConflict",
                                  "The photo was already deleted or modified.");
        }

        return Unit.Value;
    }
}
