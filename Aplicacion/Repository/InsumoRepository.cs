using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class InsumoRepository : GenericRepository<Insumo>, IInsumo
{
    private readonly ApiDbContext _context;

    public InsumoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    // public async Task<IEnumerable<Insumo>> mostrarDeterminado(int CodigoDeterminado)
    // {
    //     var determinado = await _context.Insumos
    //         .Where(m => m.Prendas.Any(c => c.Prenda.IdPrenda == CodigoDeterminado ))
    //         .Include(c => c.Insumos)
    //         .ToListAsync();

    //     return determinado;
    // }
}