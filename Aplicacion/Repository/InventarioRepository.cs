using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class InventarioRepository : GenericRepository<Inventario>, IInventario
{
    private readonly ApiDbContext _context;

    public InventarioRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}