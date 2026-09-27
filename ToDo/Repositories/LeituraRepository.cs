using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Interfaces;

namespace ToDo.Repositories
{
    public class LeituraRepository<T> : ILeituraRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public LeituraRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }
    }
}
