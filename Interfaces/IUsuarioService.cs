using ToDo.DTOs.Usuario;
using static ToDo.DTOs.Usuario.Create;
using static ToDo.DTOs.Usuario.Login;
using static ToDo.DTOs.Usuario.Response;
using static ToDo.DTOs.Usuario.Senha;
using static ToDo.DTOs.Usuario.Update;

namespace ToDo.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> CriarAsync(CreateUsuarioDto dto);
        Task<UsuarioResponseDto> UpdateAsync(int id, UpdateUsuarioDto dto);
        Task<UsuarioResponseDto> ObterPerfilAsync(int id);
        Task<UsuarioResponseDto> AlterarSenhaAsync(int id, SenhaDto dto);
        Task DesativarAsync(int id);
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}