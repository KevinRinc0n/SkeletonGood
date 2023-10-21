using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class EstadoRepository : GenericRepository<Estado>, IEstado
{
    private readonly ApiDbContext _context;

    public EstadoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}