namespace Domain.Entities.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByLoginIdentifierAsync(string loginIdentifier);
}
