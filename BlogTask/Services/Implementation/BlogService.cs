using BlogTask.Data.Repository.Implementations;
using BlogTask.Data.Repository.Interfaces;
using BlogTask.Models.Entities;
using BlogTask.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogTask.Services.Implementation
{ 
    public class BlogService : IBlogService
    {

        private readonly IRepository _repository;

        public BlogService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task AddBlog(Blog entity)
        {
           var blog = (await _repository.AddAsync<Blog>(entity));

        }

        public async Task DeleteBlog(Blog entity)
        {
            await _repository.DeleteAsync<Blog>(entity);
        }

        /* public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }*/
        public async Task<Blog?> GetBlog(string BlogId)
        {
            var getBlog = await _repository.GetAsync<Blog>();
            return getBlog.Include(x => x.Category).FirstOrDefault(x => x.Id == BlogId);
        }

        public async Task<IEnumerable<Blog>> GetAllBlog()
        {
            var blogs = (await _repository.GetAsync<Blog>())
                .Include(x => x.Category);
            var orderedBlogs = blogs.OrderByDescending(x => x.Id).ToList();
            return orderedBlogs;       
        }
        public async Task UpdateBlog(Blog entity)
        {
           await _repository.UpdateAsync(entity);
        }
    }
}
