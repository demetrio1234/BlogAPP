using Microsoft.EntityFrameworkCore;
using WebAPP.API.Data;
using WebAPP.API.Models.Domain;
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
            await dbContext.BlogCategory.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.BlogCategory.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid Id)
        {
            return await dbContext.BlogCategory.FirstOrDefaultAsync(category => category.Id == Id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category request)
        {
            Category? existingCategory = await dbContext.BlogCategory.FirstOrDefaultAsync(category => category.Id == request.Id);

            if (existingCategory == null) return null;

            dbContext.Entry(existingCategory).CurrentValues.SetValues(request);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<Category?> DeleteCategoryAsync(Guid Id)
        {
            Category? existingCategory = await dbContext.BlogCategory.FirstOrDefaultAsync(category => category.Id == Id);

            if (existingCategory == null) return null;

            dbContext.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;

        }
    }
}
