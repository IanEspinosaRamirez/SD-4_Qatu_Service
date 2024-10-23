using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Delete;

internal sealed class DeleteStoreCommandHandler
    : IRequestHandler<DeleteStoreCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStoreCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteStoreCommand command,
                                            CancellationToken cancellationToken)
    {

        await _unitOfWork.StoreRepository.Delete(new CustomerId(command.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
