using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class PrendaRepository : GenericRepository<Prenda>, IPrenda
{
    private readonly ApiDbContext _context;

    public PrendaRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}