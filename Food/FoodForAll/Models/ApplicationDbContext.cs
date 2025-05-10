using Microsoft.EntityFrameworkCore;

namespace FoodForAll.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<EstabelecimentoDoador> EstabelecimentoDoador { get; set; }
        public DbSet<EstabelecimentoReceptor> EstabelecimentoReceptor { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Doacao> Doacao { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<HistoricoTransacao> HistoricoTransacao { get; set; }
    }
} 