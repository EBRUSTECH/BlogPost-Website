using Microsoft.AspNetCore.Identity;

namespace BlogTask.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        // Navigation prop
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
    }
}
