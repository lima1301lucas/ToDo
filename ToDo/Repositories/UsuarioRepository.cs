using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _appDbContext;

        public UsuarioRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _appDbContext.Usuarios
                            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetLoginAsync(string identificador)
        {
            return await _appDbContext.Usuarios
                            .FirstOrDefaultAsync(u => u.Email == identificador || u.Username == identificador);
        }

        public async Task<bool> ExisteEmailOuUsernameAsync(string email, string username)
        {
            return await _appDbContext.Usuarios
                            .AnyAsync(u => u.Email == email || u.Username == username);
        }

        public async Task AddAsync(Usuario usuario)
        {
            _appDbContext.Usuarios.Add(usuario);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _appDbContext.Usuarios.Update(usuario);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            usuario.Ativo = false;
            _appDbContext.Usuarios.Update(usuario);
            await _appDbContext.SaveChangesAsync();
        }
    }
}