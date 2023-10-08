using WebAPP.API.Data;
using WebAPP.API.Models.Doamin;

namespace WebAPP.API.Repositories.Implementation
{
    public class BlogPostRepository
    {

        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            //new Model -> DB
            await dbContext.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        }
    }
}
