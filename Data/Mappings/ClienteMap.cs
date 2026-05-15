using LojaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaApi.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

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
        }
    }
}