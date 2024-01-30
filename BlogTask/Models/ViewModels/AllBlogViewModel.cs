using BlogTask.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BlogTask.Models.ViewModels
{
    public class AllBlogViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
