using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class TipoProteccionRepository : GenericRepository<TipoProteccion>, ITipoProteccion
{
    private readonly ApiDbContext _context;

    public TipoProteccionRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}