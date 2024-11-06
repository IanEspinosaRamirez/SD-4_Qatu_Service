using Application.Querys.User.DTOs;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Querys.User.GetById;

public record GetByIdUserQuery(CustomerId Id)
    : IRequest<ErrorOr<ResponseGetUserByIdDto>>;
