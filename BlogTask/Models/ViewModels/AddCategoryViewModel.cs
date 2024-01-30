using System.ComponentModel.DataAnnotations;

namespace BlogTask.Models.ViewModels
{
    public class AddCategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }


    }
}
