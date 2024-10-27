using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.Delete;

public record DeleteReviewProductCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
