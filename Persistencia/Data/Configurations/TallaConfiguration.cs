using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class TallaConfiguration : IEntityTypeConfiguration<Talla>
{
    public void Configure(EntityTypeBuilder<Talla> builder)
    {
        builder.ToTable("talla");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);
        
        builder.HasIndex(c => c.Descripcion)
        .IsUnique();

        builder.HasMany(p => p.Inventarios)
        .WithMany(r => r.Tallas)
        .UsingEntity<InventarioTalla>(
            
            j => j.HasOne(pt => pt.Inventario)
            .WithMany(t => t.InventariosTallas)
            .HasForeignKey(ut => ut.IdInventarioFk),

            j => j.HasOne(et => et.Talla)
            .WithMany(et => et.InventariosTallas)
            .HasForeignKey(h => h.IdTallaFk),

            j => 
            {
                j.HasKey(t => new { t.IdInventarioFk, t.IdTallaFk });
            }
        );

        builder.HasData(
            new Talla { Id = 1, Descripcion = "M"},
            new Talla { Id = 2, Descripcion = "L"},
            new Talla { Id = 3, Descripcion = "XXL"}
        );
    }
}