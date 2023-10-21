namespace Api.Dtos;

public class OrdenClienteDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public ClienteOrdenDto Cliente { get; set; }
    public EstaadoDto Estado { get; set; }
}
