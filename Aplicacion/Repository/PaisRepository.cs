using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class PaisRepository : GenericRepository<Pais>, IPais
{
    private readonly ApiDbContext _context;

    public PaisRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}