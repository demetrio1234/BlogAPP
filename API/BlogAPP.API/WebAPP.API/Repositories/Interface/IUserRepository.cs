using WebAPP.API.Models.Doamin;

namespace WebAPP.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

    }
}
