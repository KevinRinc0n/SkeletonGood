using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class InventarioRepository : GenericRepository<Inventario>, IInventario
{
    private readonly ApiDbContext _context;

    public InventarioRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inventario>> productosYTallas()
    {
        var productosTallas = await _context.Inventarios
            .Include(o => o.Prendas)
            .Include(o => o.Tallas) 
            // .Select(talla => new
            // {
            //     descripcion = talla.Count
            // })
            .ToListAsync(); 

        return productosTallas;     
    }
}