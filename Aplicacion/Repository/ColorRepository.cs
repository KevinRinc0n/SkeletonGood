using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class ColorRepository : GenericRepository<Color>, IColor
{
    private readonly ApiDbContext _context;

    public ColorRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}