using System.ComponentModel.DataAnnotations;
using WebAPP.API.Models.DTO;

namespace WebAPP.API.Models.Domain
{
    public class BlogPost : BaseBlogPost, IDomainMarker

    {
        public BlogPost()
        {

        }
        public BlogPost(BlogPostDto postDto)
        {
            Title = postDto.Title;
            ShortDescription = postDto.ShortDescription;
            Content = postDto.Content;
            FeaturedImageUrl = postDto.FeaturedImageUrl;
            UrlHandle = postDto.UrlHandle;
            PublishedDate = postDto.PublishedDate;
            Author = postDto.Author;
            IsVisible = postDto.IsVisible;
        }

        public BlogPost(Guid Id, BlogPostDto postDto)
        {
            this.Id = Id;
            Title = postDto.Title;
            ShortDescription = postDto.ShortDescription;
            Content = postDto.Content;
            FeaturedImageUrl = postDto.FeaturedImageUrl;
            UrlHandle = postDto.UrlHandle;
            PublishedDate = postDto.PublishedDate;
            Author = postDto.Author;
            IsVisible = postDto.IsVisible;
        }

        [Key]
        public Guid Id { get; set; }

    }
}


