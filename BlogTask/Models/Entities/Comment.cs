namespace BlogTask.Models.Entities
{
    public class Comment : BaseEntity
    {
        public string CommentatorId { get; set; } = "";
        public string BlogId { get; set; } = "";
        public string Text { get; set; } = "";
        public AppUser Commentator { get; set; }
        public Blog Blog { get; set; }
    }
}
