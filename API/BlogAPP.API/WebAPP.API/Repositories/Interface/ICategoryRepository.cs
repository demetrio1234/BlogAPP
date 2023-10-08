using WebAPP.API.Models.Doamin;

namespace WebAPP.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

    }
}
