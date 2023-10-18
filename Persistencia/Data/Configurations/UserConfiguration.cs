using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.Email)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.Contraseña)
        .IsRequired()
        .HasMaxLength(255);

         builder.HasMany(p => p.Roles)
            .WithMany(r => r.Usuarios)
            .UsingEntity<UserRol>(
                
                j => j.HasOne(pt => pt.Rol)
                .WithMany(t => t.RolesUsuarios)
                .HasForeignKey(ut => ut.IdRolFk),

                j => j.HasOne(et => et.Usuario)
                .WithMany(et => et.RolesUsuarios)
                .HasForeignKey(h => h.IdUsuarioFk),

                j => 
                {
                    j.HasKey(t => new { t.IdUsuarioFk, t.IdRolFk });
                }
            );

        builder.HasMany(p => p.RefreshTokens)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.IdUserFk);

        builder.HasData(
            new User { Id = 1, Nombre = "Kevin", Email = "kevin@gmail.com", Contraseña = "1234" },
            new User { Id = 2, Nombre = "user", Email = "user@gmail.com", Contraseña = "1234"  }
        );
    }
}
