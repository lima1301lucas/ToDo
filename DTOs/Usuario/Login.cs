namespace ToDo.DTOs.Usuario
{
    public class Login
    {
        public record LoginDto(
            string Identificador,
            string Senha
        );
    }
}
