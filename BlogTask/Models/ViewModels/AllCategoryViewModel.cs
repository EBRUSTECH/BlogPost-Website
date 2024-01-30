using BlogTask.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogTask.Models.ViewModels
{
    public class AllCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
