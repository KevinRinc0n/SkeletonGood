using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class CargoRepository : GenericRepository<Cargo>, ICargo
{
    private readonly ApiDbContext _context;

    public CargoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cargo>> mostrarDeterminado(string NombreDeterminado)
    {
        var determinado = await _context.Cargos
            .Where(c => c.Descripcion == NombreDeterminado)
            .Include(c => c.Empleados)
            .ToListAsync();

        return determinado;
    } 
}