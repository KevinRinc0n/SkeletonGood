namespace Api.Dtos;

public class CargoDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public double SueldoBase { get; set; }
    public ICollection<EmpleadoDto> Empleados { get; set; }
}