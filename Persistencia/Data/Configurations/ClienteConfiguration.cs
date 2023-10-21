using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("cliente");

        builder.Property(c => c.IdCliente)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasIndex(c => c.IdCliente)
        .IsUnique();

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdTipoPersonaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdMunicipioFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.FechaRegistro)
        .IsRequired();

        builder.HasOne(c => c.Municipio)
        .WithMany(c => c.Clientes)
        .HasForeignKey(c => c.IdMunicipioFk);

        builder.HasOne(c => c.TipoPersona)
        .WithMany(c => c.Clientes)
        .HasForeignKey(c => c.IdTipoPersonaFk);

        builder.HasData(
            new Cliente { Id = 1, FechaRegistro = DateTime.Now, IdCliente = "65564", Nombre = "Pri", IdTipoPersonaFk = 2, IdMunicipioFk = 2},
            new Cliente { Id = 2, FechaRegistro = DateTime.Now, IdCliente = "333", Nombre = "Maria", IdTipoPersonaFk = 1, IdMunicipioFk = 1},
            new Cliente { Id = 3, FechaRegistro = DateTime.Now, IdCliente = "777", Nombre = "Carlos", IdTipoPersonaFk = 1, IdMunicipioFk = 3}
        );
    }
}