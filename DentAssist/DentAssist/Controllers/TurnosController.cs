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
    public class TurnosController : Controller
    {
        private readonly ClinicaDbContext _context;

        public TurnosController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public async Task<IActionResult> Index()
        {
            return View(await _context.turnos.ToListAsync());
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.turnos
        .Include(t => t.Odontologo) 
        .Include(t => t.Paciente)   
        .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewBag.Pacientes = new SelectList(_context.pacientes, "Id", "Nombre");
            ViewBag.Odontologos = new SelectList(_context.odontologos, "Id", "Nombre");
            ViewBag.Estados = new SelectList(new[] { "Pendiente", "Confirmado", "Realizado", "Cancelado" });

            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Duracion,Estado,PacienteId,OdontologoId")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewBag.Estados = new SelectList(new[] { "Pendiente", "Confirmado", "Realizado", "Cancelado" });

            ViewBag.Pacientes = new SelectList(_context.pacientes, "Id", "Nombre", turno.PacienteId);
            ViewBag.Odontologos = new SelectList(_context.odontologos, "Id", "Nombre", turno.OdontologoId);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Duracion,Estado,PacienteId,OdontologoId")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }
            ViewBag.Estados = new SelectList(new[] { "Pendiente", "Confirmado", "Realizado", "Cancelado" });

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
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
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.turnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.turnos.FindAsync(id);
            if (turno != null)
            {
                _context.turnos.Remove(turno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.turnos.Any(e => e.Id == id);
        }
        
    }
}
