using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
{
    public void Configure(EntityTypeBuilder<Inventario> builder)
    {
        builder.ToTable("inventario");

        builder.Property(c => c.CodigoInv)
        .IsRequired()
        .HasColumnType("int");

        builder.HasIndex(c => c.CodigoInv)
        .IsUnique();

        builder.Property(c => c.valorVtaCop)
        .IsRequired()
        .HasColumnType("double");
        
        builder.Property(c => c.valorVtaUsd)
        .IsRequired()
        .HasColumnType("double");
        

        builder.HasData(
            new Inventario { Id = 1, CodigoInv = 12, valorVtaCop = 2.5, valorVtaUsd = 1600},
            new Inventario { Id = 2, CodigoInv = 123, valorVtaCop = 1000, valorVtaUsd = 31.333},
            new Inventario { Id = 3, CodigoInv = 24, valorVtaCop = 3900, valorVtaUsd = 42.1},
            new Inventario { Id = 4, CodigoInv = 31, valorVtaCop = 11.322, valorVtaUsd = 27000}
        );
    }
}