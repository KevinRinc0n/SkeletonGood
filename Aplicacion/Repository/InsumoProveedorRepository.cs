using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class InsumoProveedorRepository : GenericRepository<InsumoProveedor>, IInsumoProveedor
{
    private readonly ApiDbContext _context;

    public InsumoProveedorRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}