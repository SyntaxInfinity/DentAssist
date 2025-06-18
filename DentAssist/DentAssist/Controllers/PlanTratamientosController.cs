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
    public class PlanTratamientosController : Controller
    {
        private readonly ClinicaDbContext _context;

        public PlanTratamientosController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: PlanTratamientos
        public IActionResult Index(string pacienteNombre, string odontologoNombre)
        {
            var planes = _context.planTratamientos
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(pacienteNombre))
            {
                planes = planes.Where(p => p.Paciente.Nombre.Contains(pacienteNombre));
            }

            if (!string.IsNullOrEmpty(odontologoNombre))
            {
                planes = planes.Where(p => p.Odontologo.Nombre.Contains(odontologoNombre));
            }


            ViewBag.FiltroPaciente = pacienteNombre;
            ViewBag.FiltroOdontologo = odontologoNombre;

            return View(planes.ToList());
        }

        // GET: PlanTratamientos/Details/5
        public IActionResult Details(int id)
        {
            var plan = _context.planTratamientos
                .Include(p => p.Paciente)
                .Include(p => p.Odontologo)
                .FirstOrDefault(p => p.Id == id);

            if (plan == null)
                return NotFound();

            var tratamientosPropuestos = _context.tratamientosPropuestos
                .Include(tp => tp.Tratamiento)
                .Where(tp => tp.PlanId == id)
                .ToList();

            ViewBag.TratamientosPropuestos = tratamientosPropuestos;

            return View(plan);
        }



        // GET: PlanTratamientos/Create
        public IActionResult Create(int odontologoId)
        {
            ViewBag.Pacientes = new SelectList(_context.pacientes, "Id", "Nombre");

            var odontologo = _context.odontologos.Find(odontologoId);
            ViewBag.NombreOdontologo = odontologo?.Nombre;

            var plan = new PlanTratamiento
            {
                OdontologoId = odontologoId,
                FechaCreacion = DateTime.Now
            };

            return View(plan);
        }

        // POST: PlanTratamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,OdontologoId,FechaCreacion,ObservacionesGenerales")] PlanTratamiento planTratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planTratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planTratamiento);
        }

        // GET: PlanTratamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planTratamiento = await _context.planTratamientos.FindAsync(id);
            if (planTratamiento == null)
            {
                return NotFound();
            }
            return View(planTratamiento);
        }

        // POST: PlanTratamientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId,OdontologoId,FechaCreacion,ObservacionesGenerales")] PlanTratamiento planTratamiento)
        {
            if (id != planTratamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanTratamientoExists(planTratamiento.Id))
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
            return View(planTratamiento);
        }

        // GET: PlanTratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planTratamiento = await _context.planTratamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planTratamiento == null)
            {
                return NotFound();
            }

            return View(planTratamiento);
        }

        // POST: PlanTratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planTratamiento = await _context.planTratamientos.FindAsync(id);
            if (planTratamiento != null)
            {
                _context.planTratamientos.Remove(planTratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanTratamientoExists(int id)
        {
            return _context.planTratamientos.Any(e => e.Id == id);
        }
    }
}
