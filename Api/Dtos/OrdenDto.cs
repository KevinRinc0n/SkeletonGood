namespace Api.Dtos;

public class OrdenDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public EmpleadoDto Empleado { get; set; }
    public ClienteDto Cliente { get; set; } 
    public EstadoDto Estado { get; set; }
}