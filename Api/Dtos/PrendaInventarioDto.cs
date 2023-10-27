namespace Api.Dtos;

public class PrendaInventarioDto
{
    public int Id { get; set;}
    public int IdPrenda { get; set; }
    public string Nombre { get; set; }
    public double valorUnitCop { get; set; } 
    public double valorUnitUsd { get; set; }
    public InventarioPrendaDto Inventario { get; set; } 
}