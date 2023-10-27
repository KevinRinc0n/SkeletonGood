using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class InsumoProveedorRepository : GenericRepository<InsumoProveedor>, IInsumoProveedor
{
    private readonly ApiDbContext _context;

    public InsumoProveedorRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InsumoProveedor>> insumosXProveedor(string nitProveedor)
    {
        var insumosPorProveedor = await _context.InsumosProveedores
            .Where(c => c.Proveedor.NitProveedor == nitProveedor && c.Proveedor.TipoPersona.Nombre == "Juridica")
            .Include(o => o.Proveedor.Insumos) 
            .ToListAsync();

        var insumosUnicos = insumosPorProveedor
            .GroupBy(ip => ip.Proveedor.Id) 
            .Select(group => group.First()) 
            .ToList();

        return insumosUnicos;     
    }
}