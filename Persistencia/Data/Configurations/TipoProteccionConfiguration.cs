using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class TipoProteccionConfiguration : IEntityTypeConfiguration<TipoProteccion>
{
    public void Configure(EntityTypeBuilder<TipoProteccion> builder)
    {
        builder.ToTable("tipoProteccion");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasData(
            new TipoProteccion { Id = 1, Descripcion = "Acidos"},
            new TipoProteccion { Id = 2, Descripcion = "Antibalas"}
        );
    }
}