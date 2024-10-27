using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.Update;

public record UpdateReviewProductCommand(Guid Id, int Rating, string Content)
    : IRequest<ErrorOr<Unit>>;
