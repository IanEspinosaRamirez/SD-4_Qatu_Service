using Domain.Entities.ReviewStores;

namespace Infrastructure.Persistence.Repositories.Entities;

public class ReviewStoreRepository : BaseRepository<ReviewStore>,
                                     IReviewStoreRepository
{
    public ReviewStoreRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de ReviewStore
}
