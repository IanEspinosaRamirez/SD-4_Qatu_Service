using Application.Commands.Category.Dtos;
using ErrorOr;
using MediatR;

namespace Application.Commands.Category.GetById;

public record GetByIdCategoryCommand(Guid Id)
    : IRequest<ErrorOr<ResponseGetCategoryByIdDto>>;

