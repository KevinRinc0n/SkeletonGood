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

        builder.HasData(
            new DetalleOrden { Id = 1, IdOrdenFk = 1, IdColorFk = 1, IdEstadoFk = 2, IdPrendaFk = 1, CantidadProducir = 23, CantidadProducida = 2},
            new DetalleOrden { Id = 2, IdOrdenFk = 1, IdColorFk = 2, IdEstadoFk = 1, IdPrendaFk = 3, CantidadProducir = 21, CantidadProducida = 9},
            new DetalleOrden { Id = 3, IdOrdenFk = 2, IdColorFk = 4, IdEstadoFk = 3, IdPrendaFk = 4, CantidadProducir = 7, CantidadProducida = 34},
            new DetalleOrden { Id = 4, IdOrdenFk = 3, IdColorFk = 3, IdEstadoFk = 1, IdPrendaFk = 2, CantidadProducir = 65, CantidadProducida = 15}
        );
    }
}