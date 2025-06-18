namespace DentAssist.Models.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int Duracion { get; set; }
        public string Estado { get; set; }

        public int? PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int? OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }
    }
}
