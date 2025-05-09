using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FoodforAll.Models
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EstabelecimentoDoador> EstabelecimentosDoadores { get; set; }
        public DbSet<EstabelecimentoReceptor> EstabelecimentosReceptores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
