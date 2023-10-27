using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class PrendaConfiguration : IEntityTypeConfiguration<Prenda>
{
    public void Configure(EntityTypeBuilder<Prenda> builder)
    {
        builder.ToTable("prenda");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdPrenda)
        .IsRequired()
        .HasColumnType("int"); 

        builder.HasIndex(c => c.IdPrenda)
        .IsUnique();

        builder.Property(c => c.valorUnitCop)
        .IsRequired()
        .HasColumnType("double");

        builder.Property(c => c.valorUnitUsd)
        .IsRequired()
        .HasColumnType("double");

        builder.Property(c => c.IdEstadoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdGeneroFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdTipoProteccionFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Estado)
        .WithMany(c => c.Prendas)
        .HasForeignKey(c => c.IdEstadoFk);

        builder.HasOne(c => c.Genero)
        .WithMany(c => c.Prendas)
        .HasForeignKey(c => c.IdGeneroFk);

        builder.HasOne(c => c.TipoProteccion)
        .WithMany(c => c.Prendas)
        .HasForeignKey(c => c.IdTipoProteccionFk);

        builder.HasOne(c => c.Inventario)
        .WithMany(c => c.Prendas)
        .HasForeignKey(c => c.IdInventarioFk);

        builder.HasData(
            new Prenda { Id = 1, IdInventarioFk = 1, IdPrenda = 34343, Nombre = "pantalon anti acido", valorUnitCop = 4.99, valorUnitUsd =  23, IdEstadoFk = 1, IdTipoProteccionFk = 2, IdGeneroFk = 1},
            new Prenda { Id = 2, IdInventarioFk = 2, IdPrenda = 143, Nombre = "camisa", valorUnitCop = 500, valorUnitUsd =  3000, IdEstadoFk = 2, IdTipoProteccionFk = 1, IdGeneroFk = 1},
            new Prenda { Id = 3, IdInventarioFk = 4, IdPrenda = 234, Nombre = "camisa permeable", valorUnitCop = 1599, valorUnitUsd = 7500, IdEstadoFk = 3, IdTipoProteccionFk = 1, IdGeneroFk = 2},
            new Prenda { Id = 4, IdInventarioFk = 3, IdPrenda = 134343, Nombre = "guantes", valorUnitCop = 7.33, valorUnitUsd = 52.3, IdEstadoFk = 3, IdTipoProteccionFk = 2, IdGeneroFk = 1}
        );
    } 
} 