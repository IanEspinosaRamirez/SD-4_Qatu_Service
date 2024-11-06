using Application.Queries.Categories.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Queries.Categories.GetAll;

public record GetAllCategoriesQuery()
    : IRequest<ErrorOr<List<ResponseGetAllCategories>>>;
