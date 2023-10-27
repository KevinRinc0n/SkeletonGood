namespace Api.Dtos;

public class OrdenClienteDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } 
    public EstaadoDto Estado { get; set; }
}
