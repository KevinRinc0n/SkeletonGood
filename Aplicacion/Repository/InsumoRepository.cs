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
}