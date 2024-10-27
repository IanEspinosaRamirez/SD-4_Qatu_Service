using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStores.Create;

public record CreateReviewStoreCommand(int rating, string content,
                                       CustomerId userId, CustomerId storeId)
    : IRequest<ErrorOr<Unit>>;
