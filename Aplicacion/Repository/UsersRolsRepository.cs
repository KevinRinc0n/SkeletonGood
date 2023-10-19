using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class UsersRolsRepository : GenericRepository<UserRol>, IUsersRols
{
    private readonly ApiDbContext _context;

    public UsersRolsRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}