using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO;
using WebAPP.API.Repositories.Implementation;
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
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostDto request)
        {
            BlogPost blogPost = new(request);

            await blogPostRepository.CreateAsync(blogPost);

            BlogPostDto response = new(blogPost);

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            IEnumerable<BlogPost> posts = await blogPostRepository.GetAllAsync();

            List<BlogPostDto> response = new();

            foreach (BlogPost post in posts)
            {
                response.Add(new BlogPostDto(post));
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid Id)
        {
            BlogPost? blogPost = await blogPostRepository.GetByIdAsync(Id);

            if (blogPost == null) return NotFound();

            return Ok(new BlogPostDto(blogPost));
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid Id, BlogPostDto request)
        {
            BlogPost blogPost = new(Id, request);

            BlogPost? updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost == null) return NotFound();

            BlogPostDto response = new(updatedBlogPost);

            return Ok(response);

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid Id)
        {
            BlogPost? blogPost = await blogPostRepository.DeleteByIdAsync(Id);

            if (blogPost == null) return NotFound();

            BlogPostDto response = new(blogPost);

            return Ok(response);

        }
    }
}
