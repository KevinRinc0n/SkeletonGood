using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class InsumoConfiguration : IEntityTypeConfiguration<Insumo>
{
    public void Configure(EntityTypeBuilder<Insumo> builder)
    {
        builder.ToTable("insumo");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasIndex(c => c.Nombre)
        .IsUnique();

        builder.Property(c => c.ValorUnit)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.StockMin)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.StockMax)
        .IsRequired()
        .HasColumnType("int");

        builder.HasMany(p => p.Prendas)
        .WithMany(r => r.Insumos)
        .UsingEntity<InsumoPrenda>(
            
            j => j.HasOne(pt => pt.Prenda)
            .WithMany(t => t.InsumosPrendas)
            .HasForeignKey(ut => ut.IdPrendaFk),

            j => j.HasOne(et => et.Insumo)
            .WithMany(et => et.InsumosPrendas)
            .HasForeignKey(h => h.IdInsumoFk),

            j => 
            {
                j.HasKey(t => new { t.IdPrendaFk, t.IdInsumoFk });
            }
        );

        builder.HasData(
            new Insumo { Id = 1, Nombre = "tela", ValorUnit = 13.4, StockMin = 4, StockMax =  30},
            new Insumo { Id = 2, Nombre = "tela humeda", ValorUnit = 4000, StockMin = 5, StockMax =  51}, 
            new Insumo { Id = 3, Nombre = "hilo grueso", ValorUnit = 3.122, StockMin = 1, StockMax = 20},
            new Insumo { Id = 4, Nombre = "algodon resistente", ValorUnit = 2900, StockMin = 7, StockMax = 31} 
        );
    }
}