namespace DentAssist.Models.Entities
{
    public class Odontologo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int EspecialidadId { get; set; }
        public Especialidad? Especialidad { get; set; }

    }
}
