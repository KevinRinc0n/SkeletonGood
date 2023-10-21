using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class DetalleOrdenConfiguration : IEntityTypeConfiguration<DetalleOrden>
{
    public void Configure(EntityTypeBuilder<DetalleOrden> builder)
    {
        builder.ToTable("detalleOrden");

        builder.Property(c => c.IdOrdenFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdPrendaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.CantidadProducir)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdColorFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.CantidadProducida)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdEstadoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Color)
        .WithMany(c => c.DetallesOrdenes)
        .HasForeignKey(c => c.IdColorFk);

        builder.HasOne(c => c.Estado)
        .WithMany(c => c.DetallesOrdenes)
        .HasForeignKey(c => c.IdEstadoFk);

        builder.HasOne(c => c.Orden)
        .WithMany(c => c.DetallesOrdenes)
        .HasForeignKey(c => c.IdOrdenFk);

        builder.HasOne(c => c.Prenda)
        .WithMany(c => c.DetallesOrdenes)
        .HasForeignKey(c => c.IdPrendaFk);
    }
}