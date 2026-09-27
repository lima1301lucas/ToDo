using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _appDbContext;

        public TarefaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Tarefa?> GetByIdAsync(int id, int usuarioId)
        {
            return await _appDbContext.Tarefas
                            .Where(t => t.Id == id && t.UsuarioId == usuarioId)
                            .Include(t => t.Categoria)
                            .Include(t => t.Prioridade)
                            .Include(t => t.Status)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Tarefa>> GetAllByUsuarioIdAsync(int usuarioId)
        {
            return await _appDbContext.Tarefas
                            .Where(t => t.UsuarioId == usuarioId)
                            .Include(t => t.Categoria)
                            .Include(t => t.Prioridade)
                            .Include(t => t.Status)
                            .ToListAsync();
        }

        public async Task AddAsync(Tarefa tarefa)
        {
            _appDbContext.Tarefas.Add(tarefa);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Tarefa tarefa)
        {
            _appDbContext.Tarefas.Update(tarefa);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Tarefa tarefa)
        {
            _appDbContext.Tarefas.Remove(tarefa);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
