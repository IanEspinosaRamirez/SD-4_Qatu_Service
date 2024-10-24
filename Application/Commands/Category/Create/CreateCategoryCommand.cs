using ErrorOr;
using MediatR;

namespace Application.Commands.Category.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ErrorOr<Unit>>;
}
