namespace Dominio.Entities;

public class Color : BaseEntity
{
    public string Descripcion { get; set; }
    public ICollection<DetalleOrden> DetallesOrdenes { get; set; }
}