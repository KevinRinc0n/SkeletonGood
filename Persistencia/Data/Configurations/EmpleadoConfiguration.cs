using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("empleado");

        builder.Property(c => c.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(c => c.IdEmp)
        .IsRequired()
        .HasColumnType("int");

        builder.HasIndex(c => c.IdEmp)
        .IsUnique();

        builder.Property(c => c.FechaIngreso)
        .IsRequired();

        builder.Property(c => c.IdCargoFk)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(c => c.IdMunicipioFk)
        .IsRequired()
        .HasColumnType("int");

        builder.HasOne(c => c.Cargo)
        .WithMany(c => c.Empleados)
        .HasForeignKey(c => c.IdCargoFk);

        builder.HasOne(c => c.Municipio)
        .WithMany(c => c.Empleados)
        .HasForeignKey(c => c.IdMunicipioFk);

        builder.HasData(
            new Empleado { Id = 1, IdEmp = 123, Nombre = "Paco", IdCargoFk = 2, FechaIngreso = DateTime.Now, IdMunicipioFk = 1},
            new Empleado { Id = 2, IdEmp = 254, Nombre = "Kevin", IdCargoFk = 1, FechaIngreso = DateTime.Now, IdMunicipioFk = 3},
            new Empleado { Id = 3, IdEmp = 454, Nombre = "Jose", IdCargoFk = 3, FechaIngreso = DateTime.Now, IdMunicipioFk = 1},
            new Empleado { Id = 4, IdEmp = 7767, Nombre = "Fabio", IdCargoFk = 3, FechaIngreso = DateTime.Now, IdMunicipioFk = 1},
            new Empleado { Id = 5, IdEmp = 111111, Nombre = "Estela", IdCargoFk = 4, FechaIngreso = DateTime.Now, IdMunicipioFk = 3},
            new Empleado { Id = 6, IdEmp = 343, Nombre = "Andrea", IdCargoFk = 5, FechaIngreso = DateTime.Now, IdMunicipioFk = 2},
            new Empleado { Id = 7, IdEmp = 123213, Nombre = "Julian", IdCargoFk = 4, FechaIngreso = DateTime.Now, IdMunicipioFk = 3},
            new Empleado { Id = 8, IdEmp = 4343, Nombre = "Karen", IdCargoFk = 4, FechaIngreso = DateTime.Now, IdMunicipioFk = 2}
        );
    }
}