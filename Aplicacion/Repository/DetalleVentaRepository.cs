using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class DetalleVentaRepository : GenericRepository<DetalleVenta>, IDetalleVenta
{
    private readonly ApiDbContext _context;

    public DetalleVentaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}