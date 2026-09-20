using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/prioridade")]
    public class PrioridadeController : ControllerBase
    {
        private readonly ILeituraService<Prioridade> _prioridadeService;

        public PrioridadeController(ILeituraService<Prioridade> prioridadeService)
        {
            _prioridadeService = prioridadeService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterPrioridades()
        {
            var prioridades = await _prioridadeService.ObterTodosAsync();
            return Ok(prioridades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPrioridade(int id)
        {
            var prioridade = await _prioridadeService.ObterPorIdAsync(id);
            return Ok(prioridade);
        }
    }
}