using ToDo.DTOs.Tarefa;
using static ToDo.DTOs.Tarefa.Create;
using static ToDo.DTOs.Tarefa.Update;
using static ToDo.DTOs.Tarefa.Response;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        private const int StatusEmAbertoId = 1;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<TarefaResponseDto> CriarAsync(int usuarioId, CreateTarefaDto dto)
        {
            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataVencimento = dto.DataVencimento,
                UsuarioId = usuarioId,
                CategoriaId = dto.CategoriaId,
                PrioridadeId = dto.PrioridadeId,
                StatusId = StatusEmAbertoId
            };

            await _tarefaRepository.AddAsync(tarefa);

            var tarefaCriada = await _tarefaRepository.GetByIdAsync(tarefa.Id, usuarioId);

            return MapToResponseDto(tarefaCriada!);
        }

        public async Task<TarefaResponseDto> AtualizarAsync(int id, int usuarioId, UpdateTarefaDto dto)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id, usuarioId);

            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            tarefa.Titulo = dto.Titulo;
            tarefa.Descricao = dto.Descricao;
            tarefa.DataVencimento = dto.DataVencimento;
            tarefa.CategoriaId = dto.CategoriaId;
            tarefa.PrioridadeId = dto.PrioridadeId;
            tarefa.StatusId = dto.StatusId;

            await _tarefaRepository.UpdateAsync(tarefa);

            var tarefaAtualizada = await _tarefaRepository.GetByIdAsync(id, usuarioId);

            return MapToResponseDto(tarefaAtualizada!);
        }

        public async Task<TarefaResponseDto> ObterPorIdAsync(int id, int usuarioId)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id, usuarioId);

            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            return MapToResponseDto(tarefa);
        }

        public async Task<List<TarefaResponseDto>> ObterTodasAsync(int usuarioId)
        {
            var tarefas = await _tarefaRepository.GetAllByUsuarioIdAsync(usuarioId);

            return tarefas.Select(MapToResponseDto).ToList();
        }

        public async Task DeletarAsync(int id, int usuarioId)
        {
            var tarefa = await _tarefaRepository.GetByIdAsync(id, usuarioId);

            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            await _tarefaRepository.DeleteAsync(tarefa);
        }

        private static TarefaResponseDto MapToResponseDto(Tarefa tarefa)
        {
            return new TarefaResponseDto(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Descricao,
                tarefa.DataCriacao,
                tarefa.DataVencimento,
                tarefa.Categoria.Nome,
                tarefa.Prioridade.Nome,
                tarefa.Status.Nome
            );
        }
    }
}