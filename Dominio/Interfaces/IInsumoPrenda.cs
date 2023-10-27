using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IInsumoPrenda : IGenericRepository<InsumoPrenda>
{
    Task<IEnumerable<InsumoPrenda>> insumosXPrenda(int codigoPrenda);
}
