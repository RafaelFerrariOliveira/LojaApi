using Microsoft.EntityFrameworkCore;
using LojaApi.Models;
using LojaApi.Data;
using LojaApi.Maps;
using SalasReuniaoApi.Models;

namespace LojaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Vendedor> Vendedores { get; set; }
         public DbSet<SalaReuniao> SalasReuniao => Set<SalaReuniao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new VendedorMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}