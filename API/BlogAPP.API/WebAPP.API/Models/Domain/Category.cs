using System.ComponentModel.DataAnnotations;

namespace WebAPP.API.Models.Domain
{
    public class Category
    {

        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string UrlHandle { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }

    }
}
