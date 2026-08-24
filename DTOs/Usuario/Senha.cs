namespace ToDo.DTOs.Usuario
{
    public class Senha
    {
        public record SenhaDto(
            string SenhaAtual,
            string SenhaNova
        );
    }
}
