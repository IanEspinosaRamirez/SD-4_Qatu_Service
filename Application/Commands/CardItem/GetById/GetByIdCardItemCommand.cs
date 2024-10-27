using Application.Commands.CartItem.GetById.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.CartItem.GetById;

public record GetByIdCartItemCommand(Guid Id)
    : IRequest<ErrorOr<ResponseGetCartItemByIdDto>>;
