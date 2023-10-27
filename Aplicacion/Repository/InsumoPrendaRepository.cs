using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class InsumoPrendaRepository : GenericRepository<InsumoPrenda>, IInsumoPrenda
{
    private readonly ApiDbContext _context;

    public InsumoPrendaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InsumoPrenda>> insumosXPrenda(int codigoPrenda)
    {
        var insumosPorPrenda = await _context.InsumosPrendas
            .Include(c => c.Prenda)
                .ThenInclude(p => p.Insumos)
            .Where(c => c.Prenda.IdPrenda == codigoPrenda)
            .ToListAsync(); 

        return insumosPorPrenda; 
    }

}