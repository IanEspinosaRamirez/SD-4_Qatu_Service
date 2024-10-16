using Domain.Entities.Orders;

namespace Infrastructure.Persistence.Repositories.Entities;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de Order
}
