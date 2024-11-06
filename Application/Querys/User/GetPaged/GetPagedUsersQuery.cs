using Application.Querys.User.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.User.GetPaged;

public record
GetUsersPagedQuery(int PageNumber, int PageSize, string? FilterField = null,
                   string? FilterValue = null, string? OrderByField = null,
                   bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedUserDto>>>;
