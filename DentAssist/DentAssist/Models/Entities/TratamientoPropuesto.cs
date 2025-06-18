using System.ComponentModel.DataAnnotations;

namespace DentAssist.Models.Entities
{
    public class TratamientoPropuesto
    {
        [Key]
        public int Id { get; set; }

        public int PlanId { get; set; }
        public PlanTratamiento? Plan { get; set; }

        public int TratamientoId { get; set; }
        public Tratamiento? Tratamiento { get; set; }

        public DateTime FechaEstimada { get; set; }
        public string EstadoPaso { get; set; }
        public int OrdenSecuencia { get; set; }
        public string ObservacionesPaso { get; set; }
    }
}
