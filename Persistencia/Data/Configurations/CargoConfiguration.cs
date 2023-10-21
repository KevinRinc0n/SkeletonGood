using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.ToTable("cargo");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.SueldoBase)
        .IsRequired()
        .HasColumnType("double");

        builder.HasData(
            new Cargo { Id = 1, Descripcion = "Administrador", SueldoBase = 1000},
            new Cargo { Id = 2, Descripcion = "Jefe de Produccion", SueldoBase = 3.33},
            new Cargo { Id = 3, Descripcion = "Auxiliar de Bodega", SueldoBase = 77.321},
            new Cargo { Id = 4, Descripcion = "Corte, Jefe de bodega", SueldoBase = 2500},
            new Cargo { Id = 5, Descripcion = "Secretaria, Jefe de IT", SueldoBase = 800}
        );
    }
}