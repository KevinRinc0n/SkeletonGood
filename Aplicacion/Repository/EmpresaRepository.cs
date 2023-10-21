using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class EmpresaRepository : GenericRepository<Empresa>, IEmpresa
{
    private readonly ApiDbContext _context;

    public EmpresaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}