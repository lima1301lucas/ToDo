namespace ToDo.DTOs.Compartilhado
{
    public class Response
    {
        public record ItemListaResponseDto(
            int Id,
            string Nome
        );
    }
}
