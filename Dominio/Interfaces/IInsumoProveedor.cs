using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IInsumoProveedor : IGenericRepository<InsumoProveedor>
{
    Task<IEnumerable<InsumoProveedor>> insumosXProveedor(string nitProveedor);
}
