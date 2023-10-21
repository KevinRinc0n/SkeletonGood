using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("empresa");

        builder.Property(c => c.Nit)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasIndex(c => c.Nit)
        .IsUnique();

        builder.Property(c => c.RazonSocial)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.RepresentanteLegal)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.FechaCreacion)
        .IsRequired();

        builder.Property(c => c.IdMunicipioFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Municipio)
        .WithMany(c => c.Empresas)
        .HasForeignKey(c => c.IdMunicipioFk);
    }
}