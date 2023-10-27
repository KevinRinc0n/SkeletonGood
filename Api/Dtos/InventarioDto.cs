namespace Api.Dtos;

public class InventarioDto
{
    public int Id { get; set; }
    public ICollection<PrendaInventarioDto> Prendas { get; set; }
}
