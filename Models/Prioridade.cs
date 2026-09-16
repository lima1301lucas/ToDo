using ToDo.Interfaces;

namespace ToDo.Models
{
    public class Prioridade : IEntidadeNomeada
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
