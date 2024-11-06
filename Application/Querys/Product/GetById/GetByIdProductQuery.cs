using Application.Querys.Prouduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Querys.Product.GetById;

public record GetByIdProductQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetProductByIdDto>>;
