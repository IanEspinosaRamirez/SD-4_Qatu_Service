using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewProduct.Create;

public record CreateReviewProductCommand(int rating, string content,
                                       CustomerId userId, CustomerId productId)
    : IRequest<ErrorOr<Unit>>;
