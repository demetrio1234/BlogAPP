using Azure.Core;
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
            return await dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid Id)
        {
            return await dbContext.BlogPosts.FirstOrDefaultAsync(blogPost => blogPost.Id == Id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost request)
        {
            BlogPost? existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(post => post.Id == request.Id);

            if (existingBlogPost == null) return null;

            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(request);
            await dbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<BlogPost?> DeleteByIdAsync(Guid Id)
        {
            BlogPost? existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(post => post.Id == Id);

            if (existingBlogPost == null) return null;

            dbContext.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync(true);
            return existingBlogPost;
        }

    }

}