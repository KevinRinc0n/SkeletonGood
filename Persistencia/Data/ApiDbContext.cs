using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<porDefecto1> porDefectos1 { get; }
    public DbSet<porDefecto2> porDefectos2 { get; }
    public DbSet<porDefecto3> porDefectos3 { get; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UserRol> RolesUsuarios { get; set; }
    public DbSet<User> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
    }
}
