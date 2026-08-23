namespace ToDo.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime DataVencimento { get; set; }
        public int UsuarioId { get; set; }
        public int StatusId { get; set; }
        public int CategoriaId { get; set; }
        public int PrioridadeId { get; set; }
    }
}
