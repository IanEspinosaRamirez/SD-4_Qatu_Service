using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.User.Delete;

public record DeleteUserCommand(CustomerId Id) : IRequest<ErrorOr<Unit>>;
