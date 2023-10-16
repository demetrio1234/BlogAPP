using Microsoft.EntityFrameworkCore;
using WebAPP.API.Models.Domain;

namespace WebAPP.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> BlogCategory { get; set; }
    
        public DbSet<User> User { get; set; }
    }
}
