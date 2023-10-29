using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost?> GetByIdAsync(Guid Id);

        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteBlogPostAsync(Guid Id);
    }
}
