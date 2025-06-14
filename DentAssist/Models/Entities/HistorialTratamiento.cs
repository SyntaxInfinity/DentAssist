namespace DentAssist.Models.Entities
{
    public class HistorialTratamiento
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int TratamientoId { get; set; }
        public int OdontologoId { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public string Observaciones { get; set; }
    }
}
