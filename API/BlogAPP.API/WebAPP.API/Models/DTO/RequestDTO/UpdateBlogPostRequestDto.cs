namespace WebAPP.API.Models.DTO.RequestDTO
{
    public class UpdateBlogPostRequestDto
    {
        public required string Title { get; set; }

        public required string ShortDescription { get; set; }

        public required string Content { get; set; }

        public required string FeaturedImageUrl { get; set; }

        public required string UrlHandle { get; set; }

        public required DateTime PublishedDate { get; set; }

        public required string Author { get; set; }

        public required bool IsVisible { get; set; }

        public Guid[] categories { get; set; }
    }
}
