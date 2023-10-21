using Dominio.Entities;

namespace Dominio.Interfaces;

public interface ICargo : IGenericRepository<Cargo>
{
    Task<IEnumerable<Cargo>> mostrarDeterminado(string NombreDeterminado);
}
