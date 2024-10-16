using Domain.Entities.Users;

namespace Infrastructure.Persistence.Repositories.Entities;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    // Aquí puedes agregar métodos adicionales específicos de UserCustomer si es
    // necesario
}
