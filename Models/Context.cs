using API_Copa.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Selecao> Selecoes { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Palpite> Palpites { get; set; }
    }
}