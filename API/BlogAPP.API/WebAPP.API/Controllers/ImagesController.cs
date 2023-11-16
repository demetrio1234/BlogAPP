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
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file,
                                                     [FromForm] string fileName,
                                                     [FromForm] string title)
        {
            ValidateFile(file);

            if (ModelState.IsValid)
            {
                Image image = new()
                {
                    FileName = fileName,
                    FileExtension = Path.GetExtension(file.FileName).ToLowerInvariant(),
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                image = await imageRepository.UploadImage(file, image);

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

            return BadRequest(ModelState);

        }

        private void ValidateFile(IFormFile file)
        {
            var validExtensions = new string[] { ".bmp", ".jpeg", ".jpg", ".png", ".svg", };

            string extension = Path.GetExtension(file.FileName.ToLowerInvariant());

            if (!validExtensions.Contains(extension))
                ModelState.AddModelError("File Error", "Unsupported file format.");

            if (file.Length > 10485760) //File bigger than 10 MB not allowed
                ModelState.AddModelError("File Error", "Unsupported file size.");
        }

        /*
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

        */
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
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
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