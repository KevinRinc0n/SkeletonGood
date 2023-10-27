namespace Api.Dtos;

public class EmpleadoDto
{
    public int Id { get; set; }
    public int IdEmp { get; set; }
    public string Nombre { get; set; } 
    public ICollection<VentaDto> Ventas { get; set; }
}