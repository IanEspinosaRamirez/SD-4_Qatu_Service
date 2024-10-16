using Domain.Entities.ReviewProducts;

namespace Infrastructure.Persistence.Repositories.Entities;

public class ReviewProductRepository : BaseRepository<ReviewProduct>,
                                       IReviewProductRepository
{
    public ReviewProductRepository(ApplicationDbContext context)
        : base(context) { }

    // Métodos adicionales específicos de ReviewProduct
}
