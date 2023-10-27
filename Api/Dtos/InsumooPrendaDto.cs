namespace Api.Dtos;

public class InsumooPrendaDto
{
    public int Id { get; set; }    
    public int Cantidad { get; set; }    
    public PrendaDto Prenda { get; set; }
}