namespace Api.Dtos;

public class VentaDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public ICollection<DetalleVentaDto> DetallesVentas { get; set; } 
}
