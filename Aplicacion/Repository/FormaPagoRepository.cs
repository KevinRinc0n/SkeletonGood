using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;

public class FormaPagoRepository : GenericRepository<FormaPago>, IFormaPago
{
    private readonly ApiDbContext _context;

    public FormaPagoRepository(ApiDbContext context) : base(context)
    {
        _context = context;
    }
}