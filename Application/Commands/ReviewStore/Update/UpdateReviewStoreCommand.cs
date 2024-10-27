using ErrorOr;
using MediatR;

namespace Application.Commands.ReviewStore.Update;

public record UpdateReviewStoreCommand(Guid Id, int Rating, string Content)
    : IRequest<ErrorOr<Unit>>;
