using Application.Commands.User.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetById;

public record GetByIdUserCommand(Guid Id)
    : IRequest<ErrorOr<ResponseGetUserByIdDto>>;
