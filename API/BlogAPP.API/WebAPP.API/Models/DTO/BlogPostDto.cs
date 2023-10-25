using WebAPP.API.Models.Domain;

namespace WebAPP.API.Models.DTO
{
    public class BlogPostDto : BaseBlogPost
    {
        public BlogPostDto() { }
        public BlogPostDto(BlogPost post)
        {
            Id = post.Id;
            Title = post.Title;
            ShortDescription = post.ShortDescription;
            Content = post.Content;
            FeaturedImageUrl = post.FeaturedImageUrl;
            UrlHandle = post.UrlHandle;
            PublishedDate = post.PublishedDate;
            Author = post.Author;
            IsVisible = post.IsVisible;
        }
        public Guid Id { get; set; }
    }
}
