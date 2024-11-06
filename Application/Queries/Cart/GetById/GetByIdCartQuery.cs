using Application.Queries.Cart.Dto;
using ErrorOr;
using MediatR;

namespace Application.Queries.Cart.GetById;

public record GetByIdCartQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetCartByIdDto>>;
