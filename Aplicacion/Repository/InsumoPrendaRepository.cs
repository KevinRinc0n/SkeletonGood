using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class InsumoPrendaRepository : GenericRepository<InsumoPrenda>, IInsumoPrenda
{
    private readonly ApiDbContext _context;

    public InsumoPrendaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}