using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
{
    public void Configure(EntityTypeBuilder<DetalleVenta> builder)
    {
        builder.ToTable("detalleVenta");

        builder.Property(c => c.IdVentaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdInventarioFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdTallaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.Cantidad)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.ValorUnit)
        .IsRequired()
        .HasColumnType("double");

        builder.HasOne(c => c.Inventario)
        .WithMany(c => c.DetallesVentas)
        .HasForeignKey(c => c.IdInventarioFk);

        builder.HasOne(c => c.Talla)
        .WithMany(c => c.DetallesVentas)
        .HasForeignKey(c => c.IdTallaFk);

        builder.HasOne(c => c.Venta)
        .WithMany(c => c.DetallesVentas)
        .HasForeignKey(c => c.IdVentaFk);

        builder.HasData(
            new DetalleVenta { Id = 1, IdVentaFk = 1, IdInventarioFk = 1, IdTallaFk = 2, Cantidad = 16, ValorUnit = 2300},
            new DetalleVenta { Id = 2, IdVentaFk = 1, IdInventarioFk = 2, IdTallaFk = 1, Cantidad = 31, ValorUnit = 21.2},
            new DetalleVenta { Id = 3, IdVentaFk = 2, IdInventarioFk = 4, IdTallaFk = 3, Cantidad = 4, ValorUnit = 7500},
            new DetalleVenta { Id = 4, IdVentaFk = 3, IdInventarioFk = 3, IdTallaFk = 1, Cantidad = 27, ValorUnit = 65120}
        );
    }
}