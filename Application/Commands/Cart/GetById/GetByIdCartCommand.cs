using Application.Commands.Cart.Dto;
using ErrorOr;
using MediatR;

namespace Application.Commands.Cart.GetById;

public record GetByIdCartCommand(Guid Id) : IRequest<ErrorOr<ResponseGetCartByIdDto>>;
