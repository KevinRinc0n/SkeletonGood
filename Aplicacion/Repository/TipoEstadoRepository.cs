using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class TipoEstadoRepository : GenericRepository<TipoEstado>, ITipoEstado
{
    private readonly ApiDbContext _context;

    public TipoEstadoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}