using Application.Querys.Store.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.Store.GetPaged;

public record
GetStoresPagedQuery(int PageNumber, int PageSize, string? FilterField = null,
                    string? FilterValue = null, string? OrderByField = null,
                    bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedStoreDto>>>;
