namespace ToDo.DTOs.Tarefa
{
    public class Update
    {
        public record UpdateTarefaDto(
            string Titulo,
            string? Descricao,
            DateTime DataVencimento,
            int CategoriaId,
            int PrioridadeId,
            int StatusId
        );
    }
}
