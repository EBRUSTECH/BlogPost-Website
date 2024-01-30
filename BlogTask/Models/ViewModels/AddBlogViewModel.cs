using BlogTask.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BlogTask.Models.ViewModels
{
    public class AddBlogViewModel
    {
        public string Title { get; set; } = "";
        public int CategoryId { get; set; }
        public string Text { get; set; } = "";

        public IEnumerable<SelectListItem> Categories { get; set; }
        // Collect Tag
        public string[] SelectedCategories { get; set; } = Array.Empty<string>();
    }
}
