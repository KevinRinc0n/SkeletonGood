using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiDbContext _context;
    private IPorDefecto1 _porDefecto1;
    private IPorDefecto2 _porDefecto2;
    private IPorDefecto3 _porDefecto3;
    private IRol _roles;
    private IUsersRols _rolesUsuarios;
    private IUser _usuarios;

    public UnitOfWork(ApiDbContext context)
    {
        _context = context;
    }

    public IPorDefecto1 porDefectos1
    {
        get
        {
            if (_porDefecto1 == null)
            {
                _porDefecto1 = new porDefecto1Repository(_context);
            }
            return _porDefecto1;
        }
    }

    public IPorDefecto2 porDefectos2
    {
        get
        {
            if (_porDefecto2 == null)
            {
                _porDefecto2 = new porDefecto2Repository(_context);
            }
            return _porDefecto2;
        }
    }

    public IPorDefecto3 porDefectos3
    {
        get
        {
            if (_porDefecto3 == null)
            {
                _porDefecto3 = new porDefecto3Repository(_context);
            }
            return _porDefecto3;
        }
    }

    public IRol Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUsersRols RolesUsuarios
    {
        get
        {
            if (_rolesUsuarios == null)
            {
                _rolesUsuarios = new UsersRolsRepository(_context);
            }
            return _rolesUsuarios;
        }
    }

    public IUser Usuarios
    {
        get
        {
            if (_usuarios == null)
            {
                _usuarios = new UserRepository(_context);
            }
            return _usuarios;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}