using ToDo.DTOs.Compartilhado;
using static ToDo.DTOs.Compartilhado.Response;

namespace ToDo.Interfaces
{
    public interface ILeituraService<T>
    {
        Task<List<ItemListaResponseDto>> ObterTodosAsync();
        Task<ItemListaResponseDto> ObterPorIdAsync(int id);
    }
}