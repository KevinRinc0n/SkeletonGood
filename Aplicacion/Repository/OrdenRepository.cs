using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class OrdenRepository : GenericRepository<Orden>, IOrden
{
    private readonly ApiDbContext _context;

    public OrdenRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Orden>> enProceso()
    {
        var ordenesEnProceso = await _context.Ordenes
            .Where(c => c.Estado.Descripcion == "En proceso")
            .ToListAsync();

        return ordenesEnProceso;
    } 

    public async Task<IEnumerable<Orden>> ordenesClienteEspecifico(string idClientee)
    {
        var clienteEspecifico = await _context.Ordenes
            .Where(c => c.Cliente.IdCliente == idClientee)
            .ToListAsync();

        return clienteEspecifico;
    } 
}