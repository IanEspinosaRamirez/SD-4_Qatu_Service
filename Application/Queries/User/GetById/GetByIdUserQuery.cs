using Application.Queries.User.DTOs;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Queries.User.GetById;

public record GetByIdUserQuery(CustomerId Id)
    : IRequest<ErrorOr<ResponseGetUserByIdDto>>;
