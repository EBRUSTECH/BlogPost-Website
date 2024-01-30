using BlogTask.Data;
using BlogTask.Data.Repository.Interfaces;
using BlogTask.Models.Entities;
using BlogTask.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogTask.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;
     
        public CategoryService(IRepository repository)
        {
            _repository = repository;
          
        }

        public async Task AddCategory(Category entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var toDelete = await _repository.GetAsync<Category>(categoryId);
            var result = await _repository.DeleteAsync<Category>(toDelete);

            if(result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _repository.GetAsync<Category>();

            var orderedCategories = categories.OrderByDescending(c => c.CategoryId);

            return orderedCategories;
        }      

        public async Task<Category> GetCategory(int CategoryId)
        {
            return await _repository.GetAsync<Category>(CategoryId);
        }

        public async Task UpdateCategory(Category entity)
        {
            await _repository.UpdateAsync(entity);
            
        }

    }
}
