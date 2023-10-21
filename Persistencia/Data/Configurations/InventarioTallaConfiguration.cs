using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class InventarioTallaConfiguration : IEntityTypeConfiguration<InventarioTalla>
{
    public void Configure(EntityTypeBuilder<InventarioTalla> builder)
    {
        builder.ToTable("inventarioTalla");

        builder.Property(c => c.IdInventarioFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdTallaFk)
        .IsRequired()
        .HasColumnType("int");
    }
}