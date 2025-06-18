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
    public class HistorialTratamientosController : Controller
    {
        private readonly ClinicaDbContext _context;

        public HistorialTratamientosController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: HistorialTratamientos
        public async Task<IActionResult> Index()
        {
            return View(await _context.historialTratamientos.ToListAsync());
        }

        // GET: HistorialTratamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialTratamiento = await _context.historialTratamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialTratamiento == null)
            {
                return NotFound();
            }

            return View(historialTratamiento);
        }

        // GET: HistorialTratamientos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistorialTratamientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,TratamientoId,OdontologoId,FechaRealizacion,Observaciones")] HistorialTratamiento historialTratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialTratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historialTratamiento);
        }

        // GET: HistorialTratamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialTratamiento = await _context.historialTratamientos.FindAsync(id);
            if (historialTratamiento == null)
            {
                return NotFound();
            }
            return View(historialTratamiento);
        }

        // POST: HistorialTratamientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId,TratamientoId,OdontologoId,FechaRealizacion,Observaciones")] HistorialTratamiento historialTratamiento)
        {
            if (id != historialTratamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialTratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialTratamientoExists(historialTratamiento.Id))
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
            return View(historialTratamiento);
        }

        // GET: HistorialTratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialTratamiento = await _context.historialTratamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialTratamiento == null)
            {
                return NotFound();
            }

            return View(historialTratamiento);
        }

        // POST: HistorialTratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialTratamiento = await _context.historialTratamientos.FindAsync(id);
            if (historialTratamiento != null)
            {
                _context.historialTratamientos.Remove(historialTratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialTratamientoExists(int id)
        {
            return _context.historialTratamientos.Any(e => e.Id == id);
        }
    }
}
