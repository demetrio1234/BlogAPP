using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid Id);
        Task<BlogPost?> GetByUrlHandleAsync(string UrlHandle);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid Id);
    }
}
