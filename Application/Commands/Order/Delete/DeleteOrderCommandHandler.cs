using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.Delete;

internal sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.OrderRepository.Delete(new CustomerId(command.OrderId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
