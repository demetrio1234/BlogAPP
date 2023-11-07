using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<Image> CreateAsync(Image image);
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image?> GetByIdAsync(Guid Id);

        Task<Image?> UpdateAsync(Image image);

        Task<Image?> DeleteAsync(Guid Id);
    }
}
