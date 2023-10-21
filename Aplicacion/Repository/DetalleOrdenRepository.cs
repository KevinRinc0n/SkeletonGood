using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class DetalleOrdenRepository : GenericRepository<DetalleOrden>, IDetalleOrden
{
    private readonly ApiDbContext _context;

    public DetalleOrdenRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}