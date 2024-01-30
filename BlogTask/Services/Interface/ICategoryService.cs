using BlogTask.Models.Entities;

namespace BlogTask.Services.Interface
{
    public interface ICategoryService
    {
        Task AddCategory(Category entity);
        Task UpdateCategory(Category entity);
        Task<bool> DeleteCategory(int categoryId);
        Task<Category> GetCategory(int CategoryId);
        Task<IEnumerable<Category>> GetCategories();
    }
}
