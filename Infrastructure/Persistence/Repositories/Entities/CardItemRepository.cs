using Domain.Entities.CartItems;

namespace Infrastructure.Persistence.Repositories.Entities;

public class CartItemRepository : BaseRepository<CartItem>,
                                  ICartItemRepository
{
    public CartItemRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de CartItem
}
