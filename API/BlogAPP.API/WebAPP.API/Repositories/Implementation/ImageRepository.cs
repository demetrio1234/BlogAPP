using Microsoft.EntityFrameworkCore;
using WebAPP.API.Data;
using WebAPP.API.Models.Domain;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {

        private IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext dbContext;
        
        public ImageRepository(ApplicationDbContext dbContext,
            IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Image> UploadImage(IFormFile file, Image image)
        {
            //Upload the file to a local folder
            string localPath = Path.Combine(webHostEnvironment.ContentRootPath,"Images", $"{image.FileName}");//{image.FileExtension}
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //Update the database with all image's informations
            var httpRequest = httpContextAccessor.HttpContext.Request;
            string? urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}";//{image.FileExtension}

            image.Url = urlPath;

            await dbContext.Image.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;

        }

        /*
        public async Task<Image> CreateAsync(Image image)
        {
            await dbContext.Image.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
        */

        public async Task<Image?> DeleteAsync(Guid Id)
        {
            Image? existingImage = await dbContext.Image.
                    FirstOrDefaultAsync(image => image.Id == Id);

            if (existingImage == null)
                return null;

            dbContext.Remove(existingImage);
            await dbContext.SaveChangesAsync();

            return existingImage;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await dbContext.Image.ToListAsync();
        }

        public async Task<Image?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Image.FirstOrDefaultAsync(image => image.Id == Id);
        }

        public async Task<Image?> UpdateAsync(Image request)
        {
            Image? existingImage = await dbContext.Image.
                    FirstOrDefaultAsync(image => image.Id == request.Id);

            if (existingImage == null)
                return null;

            dbContext.Entry(existingImage).CurrentValues.SetValues(request);

            await dbContext.SaveChangesAsync();

            return existingImage;

        }
    }
}
