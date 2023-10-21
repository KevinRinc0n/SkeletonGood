using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class FormaPagoConfiguration : IEntityTypeConfiguration<FormaPago>
{
    public void Configure(EntityTypeBuilder<FormaPago> builder)
    {
        builder.ToTable("formaPago");

        builder.Property(c => c.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasData(
            new FormaPago { Id = 1, Descripcion = "Efectivo"},
            new FormaPago { Id = 2, Descripcion = "Tarjeta"}
        );
    }
}