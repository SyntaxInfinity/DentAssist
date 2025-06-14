namespace DentAssist.Models.Entities
{
    public class TratamientoPropuesto
    {
        public int Id { get; set; }
        public int planId { get; set; }
        public DateTime FechaEstimada { get; set; }
        public string EstadoPeso { get; set; }
        public int OrdenSecuencia { get; set; }
        public string ObservacionesPaso { get; set; }
    }
}
