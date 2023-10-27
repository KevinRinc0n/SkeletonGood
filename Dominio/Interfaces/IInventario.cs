using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IInventario : IGenericRepository<Inventario>
{
    Task<IEnumerable<Inventario>> productosYTallas();
}