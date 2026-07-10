using AtmoSync.API.Model;

namespace AtmoSync.API.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(long id);

        Task<long> CreateAsync(User user);

        Task UpdateAsync(User user);
    }
}
