using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid Id);

        Task<User?> UpdateUserAsync(User user);

        Task<User?> DeleteUserAsync(Guid Id);
    }
}
