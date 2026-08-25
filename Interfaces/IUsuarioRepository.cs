using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetLoginAsync(string identificador);
        Task<bool> ExisteEmailOuUsernameAsync(string email, string username);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
    }
}