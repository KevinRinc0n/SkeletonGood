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

    // ejemplos

    // join
    // var consulta = from persona in personas
    //             join direccion in direcciones on persona.IdPersona equals direccion.IdPersona
    //             join numeroTelefono in numerosTelefono on persona.IdPersona equals numeroTelefono.IdPersona
    //             select new
    //             {
    //                 persona.Nombre,
    //                 direccion.Ciudad,
    //                 numeroTelefono.Numero
    //             };

    // groupbyid

    // var results = persons.GroupBy(p => p.PersonID)
    //                  .Select(g => new { PersonID = g.Key, Cars = g.Select(p => p.car).ToList() });

    // var results = from p in persons
    //           group p.car by p.PersonID into g
    //           select new { PersonID = g.Key, Cars = g.ToList() };                             
}