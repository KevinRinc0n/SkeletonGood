using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class MunicipioRepository : GenericRepository<Municipio>, IMunicipio
{
    private readonly ApiDbContext _context;

    public MunicipioRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}