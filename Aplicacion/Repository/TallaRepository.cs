using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class TallaRepository : GenericRepository<Talla>, ITalla
{
    private readonly ApiDbContext _context;

    public TallaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}