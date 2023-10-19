using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class porDefecto1Repository : GenericRepository<porDefecto1>, IPorDefecto1
{
    private readonly ApiDbContext _context;

    public porDefecto1Repository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}