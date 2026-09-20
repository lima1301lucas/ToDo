using ToDo.Models;
using ToDo.Interfaces;
using static ToDo.DTOs.Usuario.Create;
using static ToDo.DTOs.Usuario.Response;
using static ToDo.DTOs.Usuario.Update;
using static ToDo.DTOs.Usuario.Senha;
using Microsoft.IdentityModel.Tokens;
using static ToDo.DTOs.Usuario.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Exceptions;

namespace ToDo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _jwtExpiraEmMinutos;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _jwtKey = configuration["Jwt:Key"]!;
            _jwtIssuer = configuration["Jwt:Issuer"]!;
            _jwtAudience = configuration["Jwt:Audience"]!;
            _jwtExpiraEmMinutos = int.Parse(configuration["Jwt:ExpiraEmMinutos"]!);
        }

        public async Task<UsuarioResponseDto> CriarAsync(CreateUsuarioDto dto)
        {
            var existe = await _usuarioRepository.ExisteEmailOuUsernameAsync(dto.Email, dto.Username);
            if (existe)
            {
                throw new ConflitoException("Email ou username já cadastrado.");
            }

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            var usuario = new Usuario
            {
                PrimeiroNome = dto.PrimeiroNome,
                Sobrenome = dto.Sobrenome,
                Username = dto.Username,
                Email = dto.Email,
                SenhaHash = senhaHash,
                Ativo = true
            };

            await _usuarioRepository.AddAsync(usuario);

            return new UsuarioResponseDto(
                usuario.Id,
                usuario.PrimeiroNome,
                usuario.Sobrenome,
                usuario.Username,
                usuario.Email
            );
        }

        public async Task<UsuarioResponseDto> UpdateAsync(int id, UpdateUsuarioDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                throw new RecursoNaoEncontradoException("Usuário não existe");
            }

            usuario.PrimeiroNome = dto.PrimeiroNome;
            usuario.Sobrenome = dto.Sobrenome;
            usuario.Username = dto.Username;
            usuario.Email = dto.Email;

            await _usuarioRepository.UpdateAsync(usuario);

            return new UsuarioResponseDto(
                usuario.Id,
                usuario.PrimeiroNome,
                usuario.Sobrenome,
                usuario.Username,
                usuario.Email
            );
        }

        public async Task<UsuarioResponseDto> ObterPerfilAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                throw new RecursoNaoEncontradoException("Usuário não existe");
            }

            return new UsuarioResponseDto(
                usuario.Id,
                usuario.PrimeiroNome,
                usuario.Sobrenome,
                usuario.Username,
                usuario.Email
            );
        }

        public async Task<UsuarioResponseDto> AlterarSenhaAsync(int id, SenhaDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                throw new RecursoNaoEncontradoException("Usuário não existe");
            }

            var senhaAtualCorreta = BCrypt.Net.BCrypt.Verify(dto.SenhaAtual, usuario.SenhaHash);

            if (!senhaAtualCorreta)
            {
                throw new NaoAutorizadoException("Senha atual incorreta");
            }

            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.SenhaNova);

            await _usuarioRepository.UpdateAsync(usuario);

            return new UsuarioResponseDto(
                usuario.Id,
                usuario.PrimeiroNome,
                usuario.Sobrenome,
                usuario.Username,
                usuario.Email
            );
        }
        public async Task DesativarAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                throw new RecursoNaoEncontradoException("Usuário não existe");
            }

            await _usuarioRepository.DeleteAsync(usuario);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var usuario = await _usuarioRepository.GetLoginAsync(dto.Identificador);

            if (usuario == null || !usuario.Ativo)
            {
                throw new NaoAutorizadoException("Usuário ou senha inválidos");
            }

            var senhaCorreta = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);

            if (!senhaCorreta)
            {
                throw new NaoAutorizadoException("Usuário ou senha inválidos");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var expiracao = DateTime.UtcNow.AddMinutes(_jwtExpiraEmMinutos);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: expiracao,
                signingCredentials: credenciais
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponseDto(tokenString, expiracao);
        }
    }
}
