using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Venta");

        builder.Property(c => c.Fecha)
        .IsRequired();

        builder.Property(c => c.IdEmpleadoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdClienteFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdFormaPagoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Cliente)
        .WithMany(c => c.Ventas)
        .HasForeignKey(c => c.IdClienteFk);

        builder.HasOne(c => c.Empleado)
        .WithMany(c => c.Ventas)
        .HasForeignKey(c => c.IdEmpleadoFk);

        builder.HasOne(c => c.FormaPago)
        .WithMany(c => c.Ventas)
        .HasForeignKey(c => c.IdFormaPagoFk);

        builder.HasData(
            new Venta { Id = 1, Fecha = DateTime.Now, IdEmpleadoFk = 1, IdClienteFk = 2, IdFormaPagoFk = 1},
            new Venta { Id = 2, Fecha = DateTime.Now, IdEmpleadoFk = 2, IdClienteFk = 1, IdFormaPagoFk = 1},
            new Venta { Id = 3, Fecha = DateTime.Now, IdEmpleadoFk = 4, IdClienteFk = 3, IdFormaPagoFk = 2}
        );
    }
}