using Domain.Entities.Stores;

namespace Infrastructure.Persistence.Repositories.Entities;

public class StoreRepository : BaseRepository<Store>, IStoreRepository
{
    public StoreRepository(ApplicationDbContext context) : base(context) { }

    // Aquí puedes agregar métodos adicionales específicos de Store si es
    // necesario
}
