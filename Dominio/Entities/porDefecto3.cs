namespace Dominio.Entities;

public class porDefecto3 : BaseEntity
{
    public DateTime FechaCita { get; set; }
    public TimeSpan HoraCita  { get; set; }
    public string Motivo { get; set; } 
}
