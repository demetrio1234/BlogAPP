﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<BlogPost?> GetByIdAsync(Guid Id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).
                FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<BlogPost?> GetByUrlHandleAsync(string UrlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).
                FirstOrDefaultAsync(x => x.UrlHandle == UrlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost request)
        {
            BlogPost? existingBlogPost = await dbContext.BlogPosts.
                Include(x => x.Categories).
                FirstOrDefaultAsync(blogPost => blogPost.Id == request.Id);

            if (existingBlogPost == null)
                return null;

            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(request);

            existingBlogPost.Categories = request.Categories;

            await dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
            BlogPost? existingBlogPost = await dbContext.BlogPosts.
                                        Include(x => x.Categories).
                                        FirstOrDefaultAsync(blogPost => blogPost.Id == Id);

            if (existingBlogPost == null)
                return null;

            dbContext.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();
            return existingBlogPost;

        }
    }
}