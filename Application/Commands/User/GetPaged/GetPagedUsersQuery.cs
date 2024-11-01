using Application.Commands.User.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetPaged;

public record
GetUsersPagedQuery(int PageNumber, int PageSize, string? FilterField = null,
                   string? FilterValue = null, string? OrderByField = null,
                   bool Ascending = true)
    : IRequest<ErrorOr<List<ResponseGetPagedUserDto>>>;
