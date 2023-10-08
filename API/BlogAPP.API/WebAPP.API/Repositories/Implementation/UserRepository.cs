using WebAPP.API.Data;
using WebAPP.API.Models.Doamin;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {

            this.dbContext = dbContext;
        }
        
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
