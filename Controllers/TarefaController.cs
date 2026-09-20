using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Interfaces;
using static ToDo.DTOs.Tarefa.Create;
using static ToDo.DTOs.Tarefa.Update;

namespace ToDo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/tarefa")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        private int ObterIdUsuarioLogado()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return int.Parse(idClaim);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateTarefaDto dto)
        {
            var usuarioId = ObterIdUsuarioLogado();
            var tarefa = await _tarefaService.CriarAsync(usuarioId, dto);
            return Created(string.Empty, tarefa);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTarefas()
        {
            var usuarioId = ObterIdUsuarioLogado();
            var tarefas = await _tarefaService.ObterTodasAsync(usuarioId);
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTarefa(int id)
        {
            var usuarioId = ObterIdUsuarioLogado();
            var tarefa = await _tarefaService.ObterPorIdAsync(id, usuarioId);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, UpdateTarefaDto dto)
        {
            var usuarioId = ObterIdUsuarioLogado();
            var tarefa = await _tarefaService.AtualizarAsync(id, usuarioId, dto);
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var usuarioId = ObterIdUsuarioLogado();
            await _tarefaService.DeletarAsync(id, usuarioId);
            return Ok();
        }
    }
}
