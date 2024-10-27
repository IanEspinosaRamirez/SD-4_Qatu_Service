using Application.Queries.Categories.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Commands.Categories.GetAll;

public record GetAllCategoriesQuery()
    : IRequest<ErrorOr<List<ResponseGetAllCategories>>>;
