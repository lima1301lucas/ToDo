using static ToDo.DTOs.Compartilhado.Response;
using ToDo.Interfaces;
using ToDo.Exceptions;

namespace ToDo.Services
{
    public class LeituraService<T> : ILeituraService<T> where T : IEntidadeNomeada
    {
        private readonly ILeituraRepository<T> _leituraRepository;

        public LeituraService(ILeituraRepository<T> leituraRepository)
        {
            _leituraRepository = leituraRepository;
        }

        public async Task<List<ItemListaResponseDto>> ObterTodosAsync()
        {
            var itens = await _leituraRepository.GetAllAsync();

            return itens
                .Select(item => new ItemListaResponseDto(item.Id, item.Nome))
                .ToList();
        }

        public async Task<ItemListaResponseDto> ObterPorIdAsync(int id)
        {
            var item = await _leituraRepository.GetByIdAsync(id);

            if (item == null)
            {
                throw new RecursoNaoEncontradoException("Item não encontrado");
            }

            return new ItemListaResponseDto(item.Id, item.Nome);
        }
    }
}