namespace ToDo.DTOs.Tarefa
{
    public class Create
    {
        public record CreateTarefaDto(
            string Titulo,
            string? Descricao,
            DateTime DataVencimento,
            int CategoriaId,
            int PrioridadeId
        );
    }
}
