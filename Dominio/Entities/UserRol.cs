namespace Dominio.Entities;

public class UserRol : BaseEntity
{
    public int IdUsuarioFk { get; set; }
    public User Usuario { get; set; }
    public int IdRolFk { get; set; }
    public Rol Rol { get; set; }
}
