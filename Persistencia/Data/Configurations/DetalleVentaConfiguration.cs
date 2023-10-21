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

        builder.Property(c => c.IdProductoFk)
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
        .HasForeignKey(c => c.IdProductoFk);

        builder.HasOne(c => c.Talla)
        .WithMany(c => c.DetallesVentas)
        .HasForeignKey(c => c.IdTallaFk);

        builder.HasOne(c => c.Venta)
        .WithMany(c => c.DetallesVentas)
        .HasForeignKey(c => c.IdVentaFk);
    }
}