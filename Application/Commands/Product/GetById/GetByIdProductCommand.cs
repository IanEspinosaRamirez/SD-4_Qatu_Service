using Application.Commands.Prouduct.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Product.GetById;

public record GetByIdProductCommand(Guid Id) : IRequest<ErrorOr<ResponseGetProductByIdDto>>;
