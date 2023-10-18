namespace Dominio.Entities;

public class porDefecto1 : BaseEntity
{
    public int IdporDefecto2Fk { get; set; }
    public porDefecto2 porDefecto2 { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public ICollection<porDefecto3> listaporDefecto3 { get; set; }
}
