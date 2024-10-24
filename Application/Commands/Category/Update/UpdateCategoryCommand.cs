using ErrorOr;
using MediatR;

namespace Application.Commands.Category.Update
{
    public record UpdateCategoryCommand(string Id, string Name) : IRequest<ErrorOr<Unit>>;
}
