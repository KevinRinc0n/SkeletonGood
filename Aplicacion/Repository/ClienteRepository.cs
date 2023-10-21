using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class ClienteRepository : GenericRepository<Cliente>, ICliente
{
    private readonly ApiDbContext _context;

    public ClienteRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}