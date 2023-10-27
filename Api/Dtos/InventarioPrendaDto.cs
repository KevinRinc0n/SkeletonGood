namespace Api.Dtos;

public class InventarioPrendaDto
{
    public int Id { get; set; }
    public ICollection<TallaDto> Tallas { get; set; }
}