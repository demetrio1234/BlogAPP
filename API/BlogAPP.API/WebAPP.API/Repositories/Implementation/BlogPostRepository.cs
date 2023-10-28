using Microsoft.EntityFrameworkCore;
using WebAPP.API.Data;
using WebAPP.API.Models.Domain;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            //new Model -> DB
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }
    }
}