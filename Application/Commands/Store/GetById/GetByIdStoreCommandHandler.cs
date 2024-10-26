using Application.Commands.Store.GetById;
using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetById;

internal sealed class GetByIdStoreCommandHandler
    : IRequestHandler<GetByIdStoreCommand, ErrorOr<ResponseGetStoreByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdStoreCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<ErrorOr<ResponseGetStoreByIdDto>> Handle(GetByIdStoreCommand request, CancellationToken cancellationToken)
    {
        var store = await _unitOfWork.StoreRepository.GetById(new CustomerId(request.Id));
        if (store is null)
        {
            return Error.Failure("Store.NotFound", "Store not found.");
        }

        var storeDto = new ResponseGetStoreByIdDto
        {
            Name = store.Name,
            Description = store.Description,
            Address = store.Address,
            CreatedAt = store.CreatedAt,
        };

        return storeDto;
    }
}
