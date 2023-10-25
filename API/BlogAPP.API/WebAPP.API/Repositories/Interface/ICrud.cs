using WebAPP.API.Models.Domain;

namespace WebAPP.API.Repositories.Interface
{
    public interface ICrud<T> where T : IDomainMarker
    {
        Task<T> CreateAsync(T model);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid Id);

        Task<T?> UpdateAsync(T model);

        Task<T?> DeleteByIdAsync(Guid Id);

    }
}
