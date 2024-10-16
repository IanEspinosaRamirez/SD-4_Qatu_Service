using Domain.Entities.Carts;

namespace Infrastructure.Persistence.Repositories.Entities;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de Cart
}
