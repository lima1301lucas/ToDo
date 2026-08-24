namespace ToDo.DTOs.Usuario
{
    public class Update
    {
        public record UpdateUsuarioDto(
            string PrimeiroNome,
            string Sobrenome,
            string Username,
            string Email
        );
    }
}