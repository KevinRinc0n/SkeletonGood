using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class porDefecto3Repository : GenericRepository<porDefecto3>, IPorDefecto3
{
    private readonly ApiDbContext _context;

    public porDefecto3Repository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}