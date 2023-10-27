using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.ToTable("municipio");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdDepartamentoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Departamento)
        .WithMany(c => c.Municipios)
        .HasForeignKey(c => c.IdDepartamentoFk);

        builder.HasData(
            new Municipio { Id = 1, Nombre = "Bucaramanga", IdDepartamentoFk = 1}, 
            new Municipio { Id = 2, Nombre = "Piedecuesta", IdDepartamentoFk = 2},
            new Municipio { Id = 3, Nombre = "Giron", IdDepartamentoFk = 1} 
        );
    }
}