namespace DentAssist.Models.Entities
{
    public class PlanTratamiento
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public string ObservacionesGenerales { get; set; }
    }
}
