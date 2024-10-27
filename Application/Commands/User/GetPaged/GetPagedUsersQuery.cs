using Application.Commands.User.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetPaged;

public record GetUsersPagedQuery(int PageNumber, int PageSize)
    : IRequest<ErrorOr<List<ResponseGetPagedUserDto>>>;
