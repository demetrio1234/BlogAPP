using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IBlogPostRepository blogPostRepository)
        {
            this.categoryRepository = categoryRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            Category category = new() { Name = request.Name, UrlHandle = request.UrlHandle };

            await categoryRepository.CreateAsync(category);

            //new Model -> new DTO as response
            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<Category> categories = await categoryRepository.GetAllCategoriesAsync();

            List<CategoryDto> response = new ();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
        {
            Category? category = await categoryRepository.GetCategoryByIdAsync(Id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDto response = new()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid Id, UpdateCategoryRequestDto request)
        {
            Category category = new() { Id = Id, Name = request.Name, UrlHandle = request.UrlHandle };

            var updatedCategory = await categoryRepository.UpdateCategoryAsync(category);

            if (updatedCategory != null)
            {
                CategoryDto response = new()
                {
                    Name = updatedCategory.Name,
                    UrlHandle = updatedCategory.UrlHandle
                };

                return Ok(response);
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid Id)
        {
            Category? category = await categoryRepository.DeleteCategoryAsync(Id);

            if (category == null)
                return NotFound();

            CategoryDto response = new() { Id = category.Id, Name = category.Name, UrlHandle = category.UrlHandle };
            return Ok(response);

        }
    }
}
