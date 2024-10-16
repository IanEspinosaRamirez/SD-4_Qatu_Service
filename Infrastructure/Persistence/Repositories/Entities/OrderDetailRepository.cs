using Domain.Entities.OrderDetails;

namespace Infrastructure.Persistence.Repositories.Entities;

public class OrderDetailRepository : BaseRepository<OrderDetail>,
                                     IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext context) : base(context) { }

    // Métodos adicionales específicos de OrderDetail si es necesario
}
