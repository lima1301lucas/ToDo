using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ITarefaRepository
    {
        Task<Tarefa?> GetByIdAsync(int id, int usuarioId);
        Task<List<Tarefa>> GetAllByUsuarioIdAsync(int usuarioId);
        Task AddAsync(Tarefa tarefa);
        Task UpdateAsync(Tarefa tarefa);
        Task DeleteAsync(Tarefa tarefa);
    }
}