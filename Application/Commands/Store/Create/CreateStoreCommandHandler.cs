using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Store.Create;

internal sealed class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateStoreCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = new Domain.Entities.Stores.Store(
            new CustomerId(Guid.NewGuid()), request.Name, request.Description,
            request.Address, new CustomerId(Guid.Parse(request.UserId)));
        await _unitOfWork.StoreRepository.Add(store);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
