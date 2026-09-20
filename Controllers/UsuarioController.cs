using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Interfaces;
using static ToDo.DTOs.Usuario.Create;
using static ToDo.DTOs.Usuario.Login;
using static ToDo.DTOs.Usuario.Senha;
using static ToDo.DTOs.Usuario.Update;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return int.Parse(idClaim);
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastrar([FromBody] CreateUsuarioDto dto)
        {
            var usuario = await _usuarioService.CriarAsync(dto);
            return Created(string.Empty, usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var resultado = await _usuarioService.LoginAsync(dto);
            return Ok(resultado);
        }

        [Authorize]
        [HttpGet("perfil")]
        public async Task<IActionResult> ObterPerfil()
        {
            var usuarioId = ObterIdUsuarioLogado();
            var usuario = await _usuarioService.ObterPerfilAsync(usuarioId);
            return Ok(usuario);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] UpdateUsuarioDto dto)
        {
            var usuarioId = ObterIdUsuarioLogado();
            var usuario = await _usuarioService.UpdateAsync(usuarioId, dto);
            return Ok(usuario);
        }

        [Authorize]
        [HttpPatch("senha")]
        public async Task<IActionResult> AtualizarSenha([FromBody] SenhaDto dto)
        {
            var usuarioId = ObterIdUsuarioLogado();
            var usuario = await _usuarioService.AlterarSenhaAsync(usuarioId, dto);
            return Ok(usuario);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Deletar()
        {
            var usuarioId = ObterIdUsuarioLogado();
            await _usuarioService.DesativarAsync(usuarioId);
            return Ok();
        }
    }
}