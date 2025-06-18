using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentAssist.Models.Data;
using DentAssist.Models.Entities;

namespace DentAssist.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ClinicaDbContext _context;

        public UsuariosController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

       
        
        public IActionResult Odontologo()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,Password,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Correo,Password,Rol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.Id == id);
        }


        public IActionResult Administrador(int? odontologoId = null)
        {
            var topTratamientos = _context.historialTratamientos
                .GroupBy(h => h.Tratamiento.Nombre)
                .Select(g => new
                {
                    Nombre = g.Key,
                    Total = g.Count()
                })
                .OrderByDescending(g => g.Total)
                .Take(5)
                .ToList();

            var odontologos = _context.odontologos
                .OrderBy(o => o.Nombre)
                .ToList();

            List<Turno> turnos = new();
            if (odontologoId != null)
            {
                turnos = _context.turnos
                    .Include(t => t.Paciente)
                    .Where(t => t.OdontologoId == odontologoId)
                    .OrderByDescending(t => t.Fecha)
                    .ToList();
            }

            ViewBag.TopTratamientos = topTratamientos;
            ViewBag.Odontologos = odontologos;
            ViewBag.SelectedOdontologoId = odontologoId;

            return View(turnos); 
        }
        /*public IActionResult Turnos(string pacienteNombre, string odontologoNombre, bool? asignado)
        {
            // Verificar si hay odontólogos registrados
            bool hayOdontologos = _context.odontologos.Any();

            var turnos = _context.turnos
                .Include(t => t.Paciente)
                .Include(t => t.Odontologo)  // Cargar relación
                .AsQueryable();

            // Filtrar por nombre de paciente (si existe)
            if (!string.IsNullOrWhiteSpace(pacienteNombre))
            {
                turnos = turnos.Where(t => t.Paciente != null && t.Paciente.Nombre.Contains(pacienteNombre));
            }

            // Filtrar por odontólogo (solo si hay registros y se proporciona un nombre)
            if (!string.IsNullOrWhiteSpace(odontologoNombre) && hayOdontologos)
            {
                turnos = turnos.Where(t => t.Odontologo != null && t.Odontologo.Nombre.Contains(odontologoNombre));
            }

            // Filtrar por asignación (con/sin odontólogo)
            if (asignado != null)
            {
                turnos = asignado.Value
                    ? turnos.Where(t => t.OdontologoId != null)  // Solo turnos asignados
                    : turnos.Where(t => t.OdontologoId == null); // Solo turnos sin asignar
            }

            // Pasar datos a la vista
            ViewBag.FiltroPaciente = pacienteNombre;
            ViewBag.FiltroOdontologo = odontologoNombre;
            ViewBag.FiltroAsignado = asignado;
            ViewBag.HayOdontologos = hayOdontologos;  // Para mostrar/ocultar opciones en la vista

            return View(turnos.ToList());
        }*/
        public IActionResult Recepcionista(string pacienteNombre, string odontologoNombre, bool? asignado)
        {
            bool hayOdontologos = _context.odontologos.Any();

            var turnos = _context.turnos
                .Include(t => t.Paciente)
                .Include(t => t.Odontologo) 
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pacienteNombre))
            {
                turnos = turnos.Where(t => t.Paciente != null && t.Paciente.Nombre.Contains(pacienteNombre));
            }

            if (!string.IsNullOrWhiteSpace(odontologoNombre) && hayOdontologos)
            {
                turnos = turnos.Where(t => t.Odontologo != null && t.Odontologo.Nombre.Contains(odontologoNombre));
            }

            if (asignado != null)
            {
                turnos = asignado.Value
                    ? turnos.Where(t => t.OdontologoId != null)  
                    : turnos.Where(t => t.OdontologoId == null);
            }
            if (!string.IsNullOrEmpty(pacienteNombre))
            {
                turnos = turnos.Where(t => t.Paciente != null && t.Paciente.Nombre.Contains(pacienteNombre));
            }

            if (!string.IsNullOrEmpty(odontologoNombre))
            {
                turnos = turnos.Where(t => t.Odontologo != null && t.Odontologo.Nombre.Contains(odontologoNombre));
            }

            if (asignado.HasValue)
            {
                turnos = asignado.Value
                    ? turnos.Where(t => t.OdontologoId != null)
                    : turnos.Where(t => t.OdontologoId == null);
            }

            var model = turnos.ToList();

            ViewBag.FiltroPaciente = pacienteNombre;
            ViewBag.FiltroOdontologo = odontologoNombre;
            ViewBag.FiltroAsignado = asignado;
            ViewBag.HayOdontologos = _context.odontologos.Any();

            return View(model);
        }
    }
}
