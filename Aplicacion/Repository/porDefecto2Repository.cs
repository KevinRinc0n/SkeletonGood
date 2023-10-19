using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class porDefecto2Repository : GenericRepository<porDefecto2>, IPorDefecto2
{
    private readonly ApiDbContext _context;

    public porDefecto2Repository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}