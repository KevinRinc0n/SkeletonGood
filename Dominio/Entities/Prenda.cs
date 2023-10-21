namespace Dominio.Entities;

public class Prenda : BaseEntity
{
    public int IdPrenda { get; set; }
    public string Nombre { get; set; }
    public double valorUnitCop { get; set; } 
    public double valorUnitUsd { get; set; }
    public int IdEstadoFk { get; set; } 
    public Estado Estado { get; set; }
    public int IdTipoProteccionFk { get; set; }
    public TipoProteccion TipoProteccion { get; set; }
    public int IdGeneroFk { get; set; }
    public Genero Genero { get; set; }
    public ICollection<InsumoPrenda> InsumosPrendas { get; set; }
    public ICollection<Inventario> Inventarios { get; set; }
    public ICollection<DetalleOrden> DetallesOrdenes { get; set; }
    public ICollection<Insumo> Insumos { get; set; }
}
