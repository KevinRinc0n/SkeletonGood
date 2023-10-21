namespace Dominio.Entities;

public class Talla : BaseEntity
{
    public string Descripcion  { get; set; }
    public ICollection<DetalleVenta> DetallesVentas { get; set; }
    public ICollection<InventarioTalla> InventariosTallas { get; set; }
    public ICollection<Inventario> Inventarios { get; set; }
}