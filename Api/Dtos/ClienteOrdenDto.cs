namespace Api.Dtos;

public class ClienteOrdenDto
{
    public int Id { get; set;}
    public string IdCliente { get; set; }
    public string Nombre { get; set; }
    public MunicipioDto Municipio { get; set; }
}
