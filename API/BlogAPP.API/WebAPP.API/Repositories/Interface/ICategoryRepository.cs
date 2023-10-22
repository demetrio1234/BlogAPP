using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid Id);
        Task<Category?> UpdateCategoryAsync(Category category);
    }
}
