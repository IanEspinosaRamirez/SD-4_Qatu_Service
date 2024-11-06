using Application.Queries.Prouduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Product.GetById;

public record GetByIdProductQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetProductByIdDto>>;
