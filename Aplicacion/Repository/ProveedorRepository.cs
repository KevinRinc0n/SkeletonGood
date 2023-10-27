using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository;

public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
{
    private readonly ApiDbContext _context;

    public ProveedorRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}