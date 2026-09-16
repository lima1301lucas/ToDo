using ToDo.DTOs.Tarefa;
using static ToDo.DTOs.Tarefa.Create;
using static ToDo.DTOs.Tarefa.Update;
using static ToDo.DTOs.Tarefa.Response;

namespace ToDo.Interfaces
{
    public interface ITarefaService
    {
        Task<TarefaResponseDto> CriarAsync(int usuarioId, CreateTarefaDto dto);
        Task<TarefaResponseDto> AtualizarAsync(int id, int usuarioId, UpdateTarefaDto dto);
        Task<TarefaResponseDto> ObterPorIdAsync(int id, int usuarioId);
        Task<List<TarefaResponseDto>> ObterTodasAsync(int usuarioId);
        Task DeletarAsync(int id, int usuarioId);
    }
}