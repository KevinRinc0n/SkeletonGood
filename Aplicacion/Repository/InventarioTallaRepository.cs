using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class InventarioTallaRepository : GenericRepository<InventarioTalla>, IInventarioTalla
{
    private readonly ApiDbContext _context;

    public InventarioTallaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}