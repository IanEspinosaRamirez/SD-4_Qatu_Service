using Application.Queries.Category.Dtos;
using ErrorOr;
using MediatR;

namespace Application.Queries.Category.GetById;

public record GetByIdCategoryQuery(Guid Id)
    : IRequest<ErrorOr<ResponseGetCategoryByIdDto>>;
