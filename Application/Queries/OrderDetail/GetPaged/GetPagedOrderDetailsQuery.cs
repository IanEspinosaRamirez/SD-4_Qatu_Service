using Application.Queries.OrderDetail.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.OrderDetail.GetPaged;

public record GetPagedOrderDetailsQuery(int PageNumber, int PageSize,
                                        string? FilterField = null,
                                        string? FilterValue = null,
                                        string? OrderByField = null,
                                        bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedOrderDetailDto>>>;
