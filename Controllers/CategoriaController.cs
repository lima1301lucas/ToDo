using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ILeituraService<Categoria> _categoriaService;

        public CategoriaController(ILeituraService<Categoria> categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterCategorias()
        {
            var categorias = await _categoriaService.ObterTodosAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCategoria(int id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            return Ok(categoria);
        }
    }
}
