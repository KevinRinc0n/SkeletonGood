using Dominio.Entities;

namespace Dominio.Interfaces;

public interface ICliente : IGenericRepository<Cliente>
{
    Task<IEnumerable<Cliente>> ordenesClienteEspecifico(string idClientee);
}
