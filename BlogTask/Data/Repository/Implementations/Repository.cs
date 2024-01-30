using BlogTask.Data.Repository.Interfaces;

namespace BlogTask.Data.Repository.Implementations
{
    public class Repository : IRepository
    {
        private readonly BlogTaskDbContext _ctx;
        public Repository(BlogTaskDbContext ctx)
        {
            _ctx = ctx;
        }
       public async Task<int> AddAsync<T>(T entity) where T : class
        {
           await _ctx.Set<T>().AddAsync(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            _ctx.Set<T>().Remove(entity);
            return await _ctx.SaveChangesAsync();
        }


        public async Task<IQueryable<T>> GetAsync<T>() where T : class
        {
            return _ctx.Set<T>().AsQueryable();
        }

       public async Task<T?> GetAsync<T>(string Id) where T : class
        {
            return await _ctx.Set<T>().FindAsync(Id);
        }

        public async Task<T?> GetAsync<T>(int Id) where T : class
        {
            return await _ctx.Set<T>().FindAsync(Id);
        }

        public async Task<int> UpdateAsync<T>(T entity) where T : class 
        {
            _ctx.Set<T>().Update(entity);
            return await _ctx.SaveChangesAsync();
        }
    }
}
