using Domain.Entities;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.Store.GetById;

internal sealed class GetByIdStoreQueryHandler
    : IRequestHandler<GetByIdStoreQuery, ErrorOr<ResponseGetStoreByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdStoreQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ResponseGetStoreByIdDto>>
    Handle(GetByIdStoreQuery query, CancellationToken cancellationToken)
    {
        var store =
            await _unitOfWork.StoreRepository.GetById(new CustomerId(query.Id));
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
