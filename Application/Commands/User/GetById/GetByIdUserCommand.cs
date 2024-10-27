using Application.Commands.User.DTOs;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.GetById;

public record GetByIdUserCommand(CustomerId Id)
    : IRequest<ErrorOr<ResponseGetUserByIdDto>>;
