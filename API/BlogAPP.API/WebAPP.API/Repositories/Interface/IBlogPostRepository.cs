using WebAPP.API.Models.Doamin;

namespace WebAPP.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {

        Task<BlogPost> CreateAsync(BlogPost blogPost);

    }
}
