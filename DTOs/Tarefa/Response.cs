namespace ToDo.DTOs.Tarefa
{
    public class Response
    {
        public record TarefaResponseDto(
            int Id,
            string Titulo,
            string? Descricao,
            DateTime DataCriacao,
            DateTime DataVencimento,
            string Categoria,
            string Prioridade,
            string Status
        );
    }
}
