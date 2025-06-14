using DentAssist.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentAssist.Models.Data
{
    public class ClinicaDbContext : DbContext
    {

        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> option) : base(option)
        { 
        }
        public DbSet<Tratamiento> administradors { get; set; }
        public DbSet<Odontologo> odontologo { get; set; }
        public DbSet<Paciente> pacientes { get; set; }
        public DbSet<Especialidad> recepciones { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<DentAssist.Models.Entities.Turno> Turno { get; set; } = default!;
        public DbSet<DentAssist.Models.Entities.TratamientoPropuesto> TratamientoPropuesto { get; set; } = default!;
        public DbSet<DentAssist.Models.Entities.HistorialTratamiento> HistorialTratamiento { get; set; } = default!;
        public DbSet<DentAssist.Models.Entities.PlanTratamiento> PlanTratamiento { get; set; } = default!;
        
    }
}
