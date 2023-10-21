namespace Api.Dtos;

public class EstaadoDto
{
    public int Id { get; set;}
    public string Descripcion { get; set; }
    public ICollection<DetallesOrdenesDto> DetallesOrdenes { get; set; }
}
