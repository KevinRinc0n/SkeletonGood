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

        builder.Property(c => c.IdPrendaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.valorVtaCop)
        .IsRequired()
        .HasColumnType("double");
        
        builder.Property(c => c.valorVtaUsd)
        .IsRequired()
        .HasColumnType("double");

        builder.HasOne(c => c.Prenda)
        .WithMany(c => c.Inventarios)
        .HasForeignKey(c => c.IdPrendaFk);
    }
}