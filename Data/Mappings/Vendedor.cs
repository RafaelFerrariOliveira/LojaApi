using LojaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaApi.Maps
{
    public class VendedorMap : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.ToTable("Vendedor");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Nome");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Email");

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Telefone");

            builder.Property(x => x.Salario)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("Salario");
        }
    }
}