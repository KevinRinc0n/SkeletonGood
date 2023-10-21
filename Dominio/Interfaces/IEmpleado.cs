using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IEmpleado : IGenericRepository<Empleado>
{
   Task<IEnumerable<Empleado>> ventas(string NombreEmpleado);

}
