using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class ClienteRepository : GenericRepository<Cliente>, ICliente
{
    private readonly ApiDbContext _context;

    public ClienteRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }

   
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