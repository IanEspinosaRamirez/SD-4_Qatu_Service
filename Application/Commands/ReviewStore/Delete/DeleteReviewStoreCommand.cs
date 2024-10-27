using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStores.Delete;

public record DeleteReviewStoreCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
