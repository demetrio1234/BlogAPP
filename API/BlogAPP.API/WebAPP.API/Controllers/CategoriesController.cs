using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO;
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto request)
        {
            Category category = new(request) ;

            category = await categoryRepository.CreateAsync(category);

            CategoryDto response = new(category);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<Category> categories = await categoryRepository.GetAllCategoriesAsync();

            List<CategoryDto> response = new List<CategoryDto>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto(category));
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
        {
            Category? category = await categoryRepository.GetCategoryByIdAsync(Id);

            if (category == null)
                return NotFound();

            return Ok(new CategoryDto(category));
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid Id, CategoryDto request)
        {
            Category category = new(Id, request);

            Category? updatedCategory = await categoryRepository.UpdateCategoryAsync(category);

            if (updatedCategory != null)
            {
                CategoryDto response = new(updatedCategory);

                return Ok(response);
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid Id)
        {
            Category? category = await categoryRepository.DeleteCategoryAsync(Id);

            if (category == null)
                return NotFound();

            CategoryDto response = new(category);

            return Ok(response);

        }
    }
}
