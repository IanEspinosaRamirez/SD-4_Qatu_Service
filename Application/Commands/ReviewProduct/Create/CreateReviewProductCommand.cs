using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.Create;

public record CreateReviewProductCommand(int rating, string content,
                                         string userId, string productId)
    : IRequest<ErrorOr<Unit>>;
