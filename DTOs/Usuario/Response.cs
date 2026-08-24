namespace ToDo.DTOs.Usuario
{
    public class Response
    {
        public record UsuarioResponseDto(
            int Id,
            string PrimeiroNome,
            string Sobrenome,
            string Username,
            string Email
        );
    }
}
