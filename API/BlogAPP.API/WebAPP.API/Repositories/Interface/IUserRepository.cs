using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
