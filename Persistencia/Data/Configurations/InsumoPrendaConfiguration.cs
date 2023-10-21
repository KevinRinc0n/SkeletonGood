using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class InsumoPrendaConfiguration : IEntityTypeConfiguration<InsumoPrenda>
{
    public void Configure(EntityTypeBuilder<InsumoPrenda> builder)
    {
        builder.ToTable("insumoPrenda");

        builder.Property(c => c.IdInsumoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdPrendaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.Cantidad)
        .IsRequired() 
        .HasColumnType("int"); 

        builder.HasData(
            new InsumoPrenda { Id = 1, IdInsumoFk = 3, IdPrendaFk = 1, Cantidad = 4},
            new InsumoPrenda { Id = 2, IdInsumoFk = 1, IdPrendaFk = 3, Cantidad = 50},
            new InsumoPrenda { Id = 3, IdInsumoFk = 2, IdPrendaFk = 4, Cantidad = 3},
            new InsumoPrenda { Id = 4, IdInsumoFk = 4, IdPrendaFk = 2, Cantidad = 21}
        );
    }
}