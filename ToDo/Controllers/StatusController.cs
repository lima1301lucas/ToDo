using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private readonly ILeituraService<Status> _statusService;

        public StatusController(ILeituraService<Status> statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterStatusList()
        {
            var statusList = await _statusService.ObterTodosAsync();
            return Ok(statusList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterStatus(int id)
        {
            var status = await _statusService.ObterPorIdAsync(id);
            return Ok(status);
        }
    }
}