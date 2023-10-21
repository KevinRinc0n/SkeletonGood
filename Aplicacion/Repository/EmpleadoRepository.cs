using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
{
    private readonly ApiDbContext _context;

    public EmpleadoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

     public async Task<IEnumerable<Empleado>> ventas(string NombreEmpleado)
    {
        var empleadoEspe = await _context.Empleados
            .Where(c => c.Nombre == NombreEmpleado)
            .Include(c => c.Ventas)
            .ToListAsync();

        return empleadoEspe;
    } 
}