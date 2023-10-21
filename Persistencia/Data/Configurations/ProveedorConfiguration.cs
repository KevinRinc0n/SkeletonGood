using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("proveedor");

        builder.Property(c => c.NitProveedor)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasIndex(c => c.NitProveedor)
        .IsUnique();

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdTipoPersonaFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdMunicipioFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.TipoPersona)
        .WithMany(c => c.Proveedores)
        .HasForeignKey(c => c.IdTipoPersonaFk);

        builder.HasMany(p => p.Insumos)
        .WithMany(r => r.Proveedores)
        .UsingEntity<InsumoProveedor>(
            
            j => j.HasOne(pt => pt.Insumo)
            .WithMany(t => t.InsumosProveedores)
            .HasForeignKey(ut => ut.IdInsumoFk),

            j => j.HasOne(et => et.Proveedor)
            .WithMany(et => et.InsumosProveedores)
            .HasForeignKey(h => h.IdProveedorFk),

            j => 
            {
                j.HasKey(t => new { t.IdInsumoFk, t.IdProveedorFk });
            }
        );

        // builder.HasData(
        //     new Proveedor { Id = 1, NitProveedor = "98989", Nombre = "Danna", IdTipoPersonaFk = 1, IdMunicipioFk = 2}, 
        //     new Proveedor { Id = 2, NitProveedor = "4343", Nombre = "Ivan", IdTipoPersonaFk = 2, IdMunicipioFk = 1},
        //     new Proveedor { Id = 3, NitProveedor = "4343434", Nombre = "Tomas", IdTipoPersonaFk = 1, IdMunicipioFk = 3}
        // );
    }
}