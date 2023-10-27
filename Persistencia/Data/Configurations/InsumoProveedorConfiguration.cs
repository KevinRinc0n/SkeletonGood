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

        builder.HasData(
            new InsumoProveedor { Id = 1, IdInsumoFk = 3, IdProveedorFk = 1},
            new InsumoProveedor { Id = 2, IdInsumoFk = 1, IdProveedorFk = 3},
            new InsumoProveedor { Id = 3, IdInsumoFk = 2, IdProveedorFk = 1},
            new InsumoProveedor { Id = 4, IdInsumoFk = 4, IdProveedorFk = 2}
        );
    }
}