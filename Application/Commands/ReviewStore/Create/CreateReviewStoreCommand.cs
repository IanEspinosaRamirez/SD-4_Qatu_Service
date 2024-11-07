using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStores.Create;

public record CreateReviewStoreCommand(int rating, string content,
                                       string userId, string storeId)
    : IRequest<ErrorOr<Unit>>;
