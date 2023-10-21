using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
{
    public void Configure(EntityTypeBuilder<Orden> builder)
    {
        builder.ToTable("orden");

        builder.Property(c => c.Fecha)
        .IsRequired();

        builder.Property(c => c.IdEmpleadoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdClienteFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdEstadoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Cliente)
        .WithMany(c => c.Ordenes)
        .HasForeignKey(c => c.IdClienteFk);

        builder.HasOne(c => c.Empleado)
        .WithMany(c => c.Ordenes)
        .HasForeignKey(c => c.IdEmpleadoFk);

        builder.HasOne(c => c.Estado)
        .WithMany(c => c.Ordenes)
        .HasForeignKey(c => c.IdEstadoFk);

        builder.HasData(
            new Orden { Id = 1, Fecha = DateTime.Now, IdEmpleadoFk = 1, IdClienteFk = 2, IdEstadoFk = 1},
            new Orden { Id = 2, Fecha = DateTime.Now, IdEmpleadoFk = 2, IdClienteFk = 1, IdEstadoFk = 1},
            new Orden { Id = 3, Fecha = DateTime.Now, IdEmpleadoFk = 2, IdClienteFk = 2, IdEstadoFk = 2}
        );
    }
}