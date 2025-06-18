using DentAssist.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace DentAssist.Models.Data
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> option) : base(option)
        {
        }
        public DbSet<Tratamiento> tratamientos { get; set; }
        public DbSet<Odontologo> odontologos { get; set; }
        public DbSet<Paciente> pacientes { get; set; }
        public DbSet<Especialidad> especialidades { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Turno> turnos { get; set; }
        public object Turnos { get; internal set; }
        public DbSet<TratamientoPropuesto> tratamientosPropuestos { get; set; }
        public DbSet<HistorialTratamiento> historialTratamientos { get; set; }
        public DbSet<PlanTratamiento> planTratamientos { get; set; }
    }
}
