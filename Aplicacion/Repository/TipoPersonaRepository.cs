using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class TipoPersonaRepository : GenericRepository<TipoPersona>, ITipoPersona
{
    private readonly ApiDbContext _context;

    public TipoPersonaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}