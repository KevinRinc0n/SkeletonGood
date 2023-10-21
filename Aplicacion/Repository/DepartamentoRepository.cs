using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
{
    private readonly ApiDbContext _context;

    public DepartamentoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}