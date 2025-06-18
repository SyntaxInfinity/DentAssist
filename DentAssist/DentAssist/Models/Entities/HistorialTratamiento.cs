namespace DentAssist.Models.Entities
{
    public class HistorialTratamiento
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int TratamientoId { get; set; }
        public Tratamiento? Tratamiento { get; set; }

        public int OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }

        public DateTime FechaRealizacion { get; set; }
        public string Observaciones { get; set; }

    }
}
