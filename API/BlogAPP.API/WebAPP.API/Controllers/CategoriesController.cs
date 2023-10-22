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

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Request -> new Model
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
            var categories = await categoryRepository.GetAllCategoriesAsync();

            var response = new List<CategoryDto>();

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


    }
}
