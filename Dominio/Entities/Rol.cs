namespace Dominio.Entities;

public class Rol : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<User> Usuarios { get; set; }  = new HashSet<User>();
    public ICollection<UserRol> RolesUsuarios { get; set; }
}
