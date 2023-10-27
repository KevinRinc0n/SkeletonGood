using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("color");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasData(
            new Color { Id = 1, Descripcion = "rojo"},
            new Color { Id = 2, Descripcion = "morado"},
            new Color { Id = 3, Descripcion = "naranja"},
            new Color { Id = 4, Descripcion = "gris"},
            new Color { Id = 5, Descripcion = "blanco"},
            new Color { Id = 6, Descripcion = "negro"}
        );
    }
}