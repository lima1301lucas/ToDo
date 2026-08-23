using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prioridade>().HasData(
                new Prioridade { Id = 1, Nome = "Baixa" },
                new Prioridade { Id = 2, Nome = "Média" },
                new Prioridade { Id = 3, Nome = "Alta" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Nome = "Em aberto" },
                new Status { Id = 2, Nome = "Finalizada" },
                new Status { Id = 3, Nome = "Cancelada" }
            );

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Trabalho" },
                new Categoria { Id = 2, Nome = "Estudos" },
                new Categoria { Id = 3, Nome = "Saúde" },
                new Categoria { Id = 4, Nome = "Finanças" },
                new Categoria { Id = 5, Nome = "Casa" },
                new Categoria { Id = 6, Nome = "Família" },
                new Categoria { Id = 7, Nome = "Lazer" },
                new Categoria { Id = 8, Nome = "Compras" },
                new Categoria { Id = 9, Nome = "Pessoal" },
                new Categoria { Id = 10, Nome = "Outros" }
            );
        }
    }
}
