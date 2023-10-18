namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IPorDefecto1 porDefectos1 { get; }
    IPorDefecto2 porDefectos2 { get; }
    IPorDefecto3 porDefectos3 { get; }
    IRol Roles { get; }
    IUsersRols RolesUsuarios { get; }
    IUser Usuarios { get; }


    Task<int> SaveAsync();
}
