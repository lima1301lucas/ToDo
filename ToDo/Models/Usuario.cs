namespace ToDo.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
