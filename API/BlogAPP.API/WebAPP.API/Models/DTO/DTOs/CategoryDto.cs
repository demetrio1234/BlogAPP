namespace WebAPP.API.Models.DTO.DTOs
{
    public class CategoryDto
    {

        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string UrlHandle { get; set; }

    }
}
