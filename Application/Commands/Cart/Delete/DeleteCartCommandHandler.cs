using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Cart.Delete;

internal sealed class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteCartCommand command,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.CartRepository.Delete(new CustomerId(command.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
