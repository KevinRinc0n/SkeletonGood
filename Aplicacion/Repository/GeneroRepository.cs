using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class GeneroRepository : GenericRepository<Genero>, IGenero
{
    private readonly ApiDbContext _context;

    public GeneroRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}