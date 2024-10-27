using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.Delete;

internal sealed class DeleteCardItemCommandHandler
    : IRequestHandler<DeleteCartItemCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCardItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCartItemCommand command,
                                            CancellationToken cancellationToken)
    {
        await _unitOfWork.CartItemRepository.Delete(new CustomerId(command.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
