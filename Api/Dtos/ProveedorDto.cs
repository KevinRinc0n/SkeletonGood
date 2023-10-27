namespace Api.Dtos;

public class ProveedorDto
{
    public int Id { get; set; }
    public string NitProveedor { get; set; }
    public string Nombre { get; set; }
    public ICollection<InsumoDto> Insumos { get; set; }
}
