using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class OrdenRepository : GenericRepository<Orden>, IOrden
{
    private readonly ApiDbContext _context;

    public OrdenRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}