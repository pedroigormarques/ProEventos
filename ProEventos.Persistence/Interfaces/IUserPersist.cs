using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Interfaces;
public interface IUserPersist : IGeralPersist
{
    public Task<IEnumerable<User>> GetUsersAsync();
    public Task<User?> GetUserByIdAsync(int id);
    public Task<User?> GetUserByUserNameAsync(string username);
}
