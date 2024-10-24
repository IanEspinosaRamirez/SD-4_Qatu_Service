using ErrorOr;
using MediatR;

namespace Application.Commands.Category.Delete;

public record DeleteCategoryCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
