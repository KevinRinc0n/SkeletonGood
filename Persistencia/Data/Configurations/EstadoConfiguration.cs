using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("estado");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdTipoEstadoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.TipoEstado)
        .WithMany(c => c.Estados)
        .HasForeignKey(c => c.IdTipoEstadoFk);

        builder.HasData(
            new Estado { Id = 1, Descripcion = "En proceso", IdTipoEstadoFk = 1},
            new Estado { Id = 2, Descripcion = "Finalizado", IdTipoEstadoFk = 1},
            new Estado { Id = 3, Descripcion = "Mandado a pedir", IdTipoEstadoFk = 2}
        );
    }
}