using BlogTask.Models.Entities;

namespace BlogTask.Services.Interface
{
    public interface IBlogService
    {
        Task AddBlog(Blog entity);
        Task UpdateBlog(Blog entity);
        Task DeleteBlog(Blog entity);
        Task<Blog?> GetBlog(string BlogId);
        Task<IEnumerable<Blog>> GetAllBlog();
      
    }
}
