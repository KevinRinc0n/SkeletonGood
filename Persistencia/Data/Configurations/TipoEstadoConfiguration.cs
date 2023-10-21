using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class TipoEstadoConfiguration : IEntityTypeConfiguration<TipoEstado>
{
    public void Configure(EntityTypeBuilder<TipoEstado> builder)
    {
        builder.ToTable("tipoEstado");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasData(
            new TipoEstado { Id = 1, Descripcion = "Nuevo"},
            new TipoEstado { Id = 2, Descripcion = "Segunda mano"}
        );
    }
}