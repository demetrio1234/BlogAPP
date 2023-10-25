using WebAPP.API.Models.Domain;

namespace WebAPP.API.Models.DTO
{
    public class CategoryDto : BaseCategory
    {
        public CategoryDto() { }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            UrlHandle = category.UrlHandle;
        }

        public Guid Id { get; set; }

    }
}
