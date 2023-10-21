namespace Dominio.Entities;

public class Inventario : BaseEntity
{
    public int CodigoInv { get; set; }
    public int IdPrendaFk { get; set; }
    public Prenda Prenda { get; set; }
    public double valorVtaCop { get; set; }
    public double valorVtaUsd { get; set; }
    public ICollection<DetalleVenta> DetallesVentas { get; set; }
    public ICollection<InventarioTalla> InventariosTallas { get; set; }
    public ICollection<Talla> Tallas { get; set; }
}