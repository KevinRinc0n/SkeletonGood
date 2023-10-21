using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona>
{
    public void Configure(EntityTypeBuilder<TipoPersona> builder)
    {
        builder.ToTable("tipoPersona");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasData(
            new TipoPersona { Id = 1, Nombre = "Juridica"},
            new TipoPersona { Id = 2, Nombre = "Del comun"},
            new TipoPersona { Id = 3, Nombre = "Fuerzas armadas"}
        );
    }
}