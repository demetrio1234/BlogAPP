using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Data;
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

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            //Request -> new Model
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
            };

            //new Model -> DB
            await blogPostRepository.CreateAsync(blogPost);

            //new Model -> new DTO as response
            var response = new BlogPostDto
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
            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            IEnumerable<BlogPost> posts = await blogPostRepository.GetAllAsync();
            
            List<BlogPostDto> response = new ();

            foreach (BlogPost post in posts)
            {
                response.Add(new BlogPostDto { 
                    
                    Id = post.Id,
                    Title = post.Title,
                    ShortDescription = post.ShortDescription ,
                    Content = post.Content,
                    FeaturedImageUrl =  post.FeaturedImageUrl,
                    UrlHandle =  post.UrlHandle,
                    PublishedDate =  post.PublishedDate,
                    Author =  post.Author,
                    IsVisible = post.IsVisible,
                });
            }
            return Ok(response);
        }
    }
}
