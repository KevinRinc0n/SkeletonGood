using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class UsersRolsConfiguration : IEntityTypeConfiguration<UserRol>
{
    public void Configure(EntityTypeBuilder<UserRol> builder)
    {
        builder.ToTable("usersRols");

        builder.Property(c => c.IdRolFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdUsuarioFk)
        .IsRequired()
        .HasColumnType("int");
    }
}
