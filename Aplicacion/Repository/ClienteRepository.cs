using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class ClienteRepository : GenericRepository<Cliente>, ICliente
{
    private readonly ApiDbContext _context;

    public ClienteRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ordenesClienteEspecifico(string idClientee)
    {
        var clienteEspecifico = await _context.Clientes
            .Include(o => o.Municipio) 
            .Include(o => o.Ordenes) 
                .ThenInclude(o => o.Estado) 
                    .ThenInclude(o => o.DetallesOrdenes) 
                        .ThenInclude(o => o.Prenda) 
            .Where(c => c.IdCliente == idClientee)
            .ToListAsync();

        return clienteEspecifico;
    }
}