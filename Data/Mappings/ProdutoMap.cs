using LojaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaApi.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Nome");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Descricao");

            builder.Property(x => x.Preco)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Preco");
        }
    }
}