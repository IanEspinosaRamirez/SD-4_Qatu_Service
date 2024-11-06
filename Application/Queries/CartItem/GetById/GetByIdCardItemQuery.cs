using Application.Queries.CartItem.GetById.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.CartItem.GetById;

public record GetByIdCartItemQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetCartItemByIdDto>>;
