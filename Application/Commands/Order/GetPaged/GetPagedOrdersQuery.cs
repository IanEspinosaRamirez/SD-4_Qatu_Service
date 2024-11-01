using Application.Commands.Order.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Order.GetPaged;

public record
GetPagedOrdersQuery(int PageNumber, int PageSize, string? FilterField = null,
                    string? FilterValue = null, string? OrderByField = null,
                    bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedOrderDto>>>;
