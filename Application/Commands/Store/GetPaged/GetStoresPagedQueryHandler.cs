using Application.Commands.Store.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Commands.Store.GetPaged;

internal sealed class GetStoresPagedQueryHandler
    : IRequestHandler<GetStoresPagedQuery, ErrorOr<List<ResponseGetPagedStoreDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStoresPagedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedStoreDto>>> Handle(
        GetStoresPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        var stores = await _unitOfWork.StoreRepository.GetPaged(
            request.PageNumber,
            request.PageSize
        );

        if (!stores.Any())
        {
            return Error.Failure("Store.NoRecords", "No stores found.");
        }

        var storeDtos = stores
            .Select(store => new ResponseGetPagedStoreDto
            {
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                UserId = store.UserId.ToString(),
                CreatedAt = store.CreatedAt,
                UpdatedAt = store.UpdatedAt,
            })
            .ToList();

        return storeDtos;
    }
}
