using Azure;

namespace BlogTask.Models.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; } = "";
        public int CategoryId { get; set; }
        public string Text { get; set; } = "";

        /* public string ImageUrl { get; set; }
        public string Author { get; set; }*/     
        public Category Category { get; set; }       
    }
}
