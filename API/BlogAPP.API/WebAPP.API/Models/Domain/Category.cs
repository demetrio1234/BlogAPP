using System.ComponentModel.DataAnnotations;
using WebAPP.API.Models.DTO;

namespace WebAPP.API.Models.Domain
{
    public class Category : BaseCategory, IDomainMarker
    {
        public Category() { }

        public Category(CategoryDto categoryDto)
        {
            Name = categoryDto.Name;
            UrlHandle = categoryDto.UrlHandle;
        }

        public Category(Guid Id, CategoryDto categoryDto)
        {
            this.Id = Id;
            Name = categoryDto.Name;
            UrlHandle = categoryDto.UrlHandle;
        }

        [Key]
        public Guid Id { get; set; }

    }
}
