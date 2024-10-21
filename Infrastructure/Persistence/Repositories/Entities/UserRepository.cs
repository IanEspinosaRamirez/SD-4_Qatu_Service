using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Entities;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<User?>
    GetUserByLoginIdentifierAsync(string loginIdentifier)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(
            user => user.Email == loginIdentifier ||
                    user.Username == loginIdentifier ||
                    user.Phone == loginIdentifier);
    }
}
