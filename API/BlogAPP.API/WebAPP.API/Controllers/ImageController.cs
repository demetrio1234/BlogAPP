using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateImageRequestDto request)
        {
            Models.Domain.Image image = new()
            {
                FileName = request.FileName,
                FileExtension = request.FileExtension,
                Title = request.Title,
                Url = request.Url,
                DateCreated = request.DateCreated,
            };

            await imageRepository.CreateAsync(image);

            ImageDto response = new()
            {
                Id = image.Id,
                FileName = image.FileName,
                FileExtension = image.FileExtension,
                Title = image.Title,
                Url = image.Url,
                DateCreated = image.DateCreated
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Models.Domain.Image> images = await imageRepository.GetAllAsync();

            List<ImageDto> response = new();

            foreach (Models.Domain.Image image in images)
            {
                response.Add(new ImageDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    Url = image.Url,
                    DateCreated = image.DateCreated
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            Models.Domain.Image? existingImage = await imageRepository.GetByIdAsync(Id);

            if (existingImage == null)
                return NotFound();

            ImageDto response = new()
            {
                FileName = existingImage.FileName,
                FileExtension = existingImage.FileExtension,
                Title = existingImage.Title,
                Url = existingImage.Url,
                DateCreated = existingImage.DateCreated
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid Id)
        {
            Models.Domain.Image? existingImage = await imageRepository.DeleteAsync(Id);

            if (existingImage == null)
                return NotFound();

            ImageDto response = new()
            {
                Id = existingImage.Id,
                FileName = existingImage.FileName,
                FileExtension = existingImage.FileExtension,
                Title = existingImage.Title,
                Url = existingImage.Url,
                DateCreated = existingImage.DateCreated,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("Id:Guid")]
        public async Task<IActionResult?> UpdateAsync([FromRoute] Guid Id, UpdateImageRequest request)
        {
            Models.Domain.Image? existingImage = new()
            {
                Id = Id,
                FileName = request.FileName,
                FileExtension = request.FileExtension,
                Title = request.Title,
                Url = request.Url,
                DateCreated = request.DateCreated
            };

            if (existingImage == null)
                return null;

            await imageRepository.UpdateAsync(existingImage);

            ImageDto response = new()
            {
                Id = existingImage.Id,
                FileName = existingImage.FileName,
                FileExtension = existingImage.FileExtension,
                Title = existingImage.Title,
                Url = existingImage.Url,
                DateCreated = existingImage.DateCreated
            };

            return Ok(response);
        }
    }
}