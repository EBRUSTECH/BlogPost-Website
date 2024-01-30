namespace BlogTask.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public string Name { get; set; } = "";    
       
    }
}
