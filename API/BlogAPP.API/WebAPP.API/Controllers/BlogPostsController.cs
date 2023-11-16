using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost()
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (Guid guid in request.categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(guid);

                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }

            }

            await blogPostRepository.CreateAsync(blogPost);

            BlogPostDto response = new()
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                })
                .ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<BlogPost> blogPosts = await blogPostRepository.GetAllAsync();

            List<BlogPostDto> response = new();

            foreach (BlogPost blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {

                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    })
                .ToList()
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            BlogPost? existingBlogPost = await blogPostRepository.GetByIdAsync(Id);

            if (existingBlogPost == null)
                return NotFound();

            BlogPostDto response = new()
            {
                Id = existingBlogPost.Id,
                Title = existingBlogPost.Title,
                ShortDescription = existingBlogPost.ShortDescription,
                Content = existingBlogPost.Content,
                FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                UrlHandle = existingBlogPost.UrlHandle,
                PublishedDate = existingBlogPost.PublishedDate,
                Author = existingBlogPost.Author,
                IsVisible = existingBlogPost.IsVisible,
                Categories = existingBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                })
                .ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{UrlHandle}")]
        public async Task<IActionResult> GetByUrlHandleAsync([FromRoute] string UrlHandle)
        {
            BlogPost? existingBlogPost = await blogPostRepository.GetByUrlHandleAsync(UrlHandle);

            if (existingBlogPost == null)
                return NotFound();

            BlogPostDto response = new()
            {
                Id = existingBlogPost.Id,
                Title = existingBlogPost.Title,
                ShortDescription = existingBlogPost.ShortDescription,
                Content = existingBlogPost.Content,
                FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                UrlHandle = existingBlogPost.UrlHandle,
                PublishedDate = existingBlogPost.PublishedDate,
                Author = existingBlogPost.Author,
                IsVisible = existingBlogPost.IsVisible,
                Categories = existingBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                })
                .ToList()
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid Id, UpdateBlogPostRequestDto request)
        {
            BlogPost blogPost = new()
            {
                Id = Id,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryGuid);
                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            BlogPost? updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost != null)
            {
                BlogPostDto response = new()
                {
                    Title = updatedBlogPost.Title,
                    ShortDescription = updatedBlogPost.ShortDescription,
                    Content = updatedBlogPost.Content,
                    FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl,
                    UrlHandle = updatedBlogPost.UrlHandle,
                    PublishedDate = updatedBlogPost.PublishedDate,
                    Author = updatedBlogPost.Author,
                    IsVisible = updatedBlogPost.IsVisible,
                    Categories = new List<CategoryDto>()
                };

                foreach (var category in updatedBlogPost.Categories)
                {
                    response.Categories.Add(new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        UrlHandle = category.UrlHandle,
                    });
                }
                return Ok(response);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid Id)
        {
            BlogPost? existingBlogPost = await blogPostRepository.DeleteAsync(Id);

            if (existingBlogPost == null)
                return NotFound();

            BlogPostDto response = new()
            {
                Title = existingBlogPost.Title,
                ShortDescription = existingBlogPost.ShortDescription,
                Content = existingBlogPost.Content,
                FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                UrlHandle = existingBlogPost.UrlHandle,
                PublishedDate = existingBlogPost.PublishedDate,
                Author = existingBlogPost.Author,
                IsVisible = existingBlogPost.IsVisible,
                Categories = new List<CategoryDto>()
            };

            foreach (var category in existingBlogPost.Categories)
            {
                response.Categories.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }
    }
}
