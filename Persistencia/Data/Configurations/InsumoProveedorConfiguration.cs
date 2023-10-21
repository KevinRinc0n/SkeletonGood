using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class InsumoProveedorConfiguration : IEntityTypeConfiguration<InsumoProveedor>
{
    public void Configure(EntityTypeBuilder<InsumoProveedor> builder)
    {
        builder.ToTable("insumoProveedor");

        builder.Property(c => c.IdInsumoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdProveedorFk)
        .IsRequired()
        .HasColumnType("int");
    }
}