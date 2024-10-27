using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.Create;

internal sealed class CreateReviewProductCommandHandler
    : IRequestHandler<CreateReviewProductCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        CreateReviewProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var reviewProduct = new Domain.Entities.ReviewProducts.ReviewProduct(
            new CustomerId(Guid.NewGuid()),
            command.rating,
            command.content,
            command.userId,
            command.productId
        );

        await _unitOfWork.ReviewProductRepository.Add(reviewProduct);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
