using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("departamento");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdPaisFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Pais)
        .WithMany(c => c.Departamentos)
        .HasForeignKey(c => c.IdPaisFk);

        builder.HasData(
            new Departamento { Id = 1, Nombre = "Santander", IdPaisFk = 1},
            new Departamento { Id = 2, Nombre = "Antioquia", IdPaisFk = 2}
        );
    }
}