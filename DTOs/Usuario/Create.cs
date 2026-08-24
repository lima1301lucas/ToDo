namespace ToDo.DTOs.Usuario
{
    public class Create
    {
        public record CreateUsuarioDto(
            string PrimeiroNome,
            string Sobrenome,
            string Username,
            string Email,
            string Senha
        );
    }
}
