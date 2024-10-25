using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Update;

internal sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateStoreCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateStoreCommand command,
                                            CancellationToken cancellationToken)
    {
        var store = new Domain.Entities.Stores.Store(
             new CustomerId(command.Id), command.Name, command.Description,
             command.Address, new CustomerId(command.UserId));


        await _unitOfWork.StoreRepository.Update(store);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
