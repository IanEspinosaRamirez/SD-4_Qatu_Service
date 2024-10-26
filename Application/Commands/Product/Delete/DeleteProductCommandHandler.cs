using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.Delete;

internal sealed class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand command,
                                            CancellationToken cancellationToken)
    {

        await _unitOfWork.ProductRepository.Delete(new CustomerId(command.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

