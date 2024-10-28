using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using Google.Cloud.Storage.V1;
using MediatR;

namespace Application.Queries.Photo.GetPhotoUrl;

internal sealed class GetByIdPhotoQueryHandler : IRequestHandler<GetByIdPhotoQuery, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly StorageClient _storageClient;
    private const string BucketName = "finnisimoqatu";

    public GetByIdPhotoQueryHandler(IUnitOfWork unitOfWork, StorageClient storageClient)
    {
        _unitOfWork = unitOfWork;
        _storageClient = storageClient;
    }

    public async Task<ErrorOr<string>> Handle(
        GetByIdPhotoQuery command,
        CancellationToken cancellationToken
    )
    {
        var photo = await _unitOfWork.PhotoRepository.GetById(new CustomerId(command.PhotoId));
        if (photo == null)
        {
            return Error.NotFound("Photo.NotFound", "Photo not found.");
        }

        // Obtener el nombre del objeto en el bucket
        var objectName = photo.ImageURL.Replace(
            $"https://storage.googleapis.com/{BucketName}/",
            ""
        );

        // Generar una URL firmada con expiración de 3 horas
        UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(
            "/home/acer/key/chromatic-being-438520-e5-309b0479f37e.json"
        ); // Ruta a tu JSON de cuenta de servicio
        var signedUrl = urlSigner.Sign(
            BucketName,
            objectName,
            TimeSpan.FromHours(3) // Expiración de 3 horas
        );

        return signedUrl;
    }
}
