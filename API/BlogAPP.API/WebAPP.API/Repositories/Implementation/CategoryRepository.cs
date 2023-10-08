using WebAPP.API.Data;
using WebAPP.API.Models.Doamin;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            //new Model -> DB
            await dbContext.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
