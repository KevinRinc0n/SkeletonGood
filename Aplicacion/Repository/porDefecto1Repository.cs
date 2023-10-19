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

    // public async Task<IEnumerable<porDefecto1>> mostrarDeterminado(string NombreDeterminado)
    // {
    //     var determinado = await _context.porDefectos1
    //         .Where(m => m.Citas.Any(c => c.porDefecto1.Nombre == NombreDeterminado ))
    //         .Include(c => c.porDefectos2)
    //         .ThenInclude(c => c.porDefecto1)
    //         .ToListAsync();

    //     return determinado;
    // }

    // var results = from p in porDefectos1
    //           group p.cosa by p.Id into g
    //           select new { Id = g.Key, Cosas = g.ToList() };
}