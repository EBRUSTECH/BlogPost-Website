using BlogTask.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogTask.Data
{
    public class BlogTaskDbContext : IdentityDbContext<AppUser>
    {
        public BlogTaskDbContext(DbContextOptions<BlogTaskDbContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
