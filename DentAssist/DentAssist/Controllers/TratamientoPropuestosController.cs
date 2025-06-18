using DentAssist.Models.Data;
using DentAssist.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace DentAssist.Controllers
{
    public class TratamientoPropuestosController : Controller
    {
        private readonly ClinicaDbContext _context;

        public TratamientoPropuestosController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: TratamientoPropuestos
        public async Task<IActionResult> Index()
        {
            return View(await _context.tratamientosPropuestos.ToListAsync());
        }

        // GET: TratamientoPropuestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoPropuesto = await _context.tratamientosPropuestos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tratamientoPropuesto == null)
            {
                return NotFound();
            }

            return View(tratamientoPropuesto);
        }

        // GET: TratamientoPropuestos/Create
        public IActionResult Create(int planId)
        {
            ViewBag.PlanId = planId;

            ViewBag.Tratamientos = new SelectList(
                _context.tratamientos.ToList(),
                "Id", "Nombre"
            );

            ViewBag.Estados = new SelectList(new[] { "Pendiente", "Realizado", "Cancelado" });

            return View();
        }


        // POST: TratamientoPropuestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TratamientoPropuesto tratamientoPropuesto)
        {
            tratamientoPropuesto.PlanId = tratamientoPropuesto.Id;
            tratamientoPropuesto.Id = 0;
            if (ModelState.IsValid)
            {
                
                _context.tratamientosPropuestos.Add(tratamientoPropuesto);
                _context.SaveChanges();
                return RedirectToAction("Details", "TratamientoPropuestos", new { id = tratamientoPropuesto.Id });
            }

            ViewBag.Tratamientos = new SelectList(_context.tratamientos, "Id", "Nombre", tratamientoPropuesto.TratamientoId);
            ViewBag.Estados = new SelectList(new[] { "Pendiente", "Realizado", "Cancelado" }, tratamientoPropuesto.EstadoPaso);
            ViewBag.PlanId = tratamientoPropuesto.Id;

            return View(tratamientoPropuesto);
        }

        // GET: TratamientoPropuestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoPropuesto = await _context.tratamientosPropuestos.FindAsync(id);
            if (tratamientoPropuesto == null)
            {
                return NotFound();
            }
            return View(tratamientoPropuesto);
        }

        // POST: TratamientoPropuestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,planId,FechaEstimada,EstadoPeso,OrdenSecuencia,ObservacionesPaso")] TratamientoPropuesto tratamientoPropuesto)
        {
            if (id != tratamientoPropuesto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamientoPropuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientoPropuestoExists(tratamientoPropuesto.Id))
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
            return View(tratamientoPropuesto);
        }

        // GET: TratamientoPropuestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoPropuesto = await _context.tratamientosPropuestos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tratamientoPropuesto == null)
            {
                return NotFound();
            }

            return View(tratamientoPropuesto);
        }

        // POST: TratamientoPropuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamientoPropuesto = await _context.tratamientosPropuestos.FindAsync(id);
            if (tratamientoPropuesto != null)
            {
                _context.tratamientosPropuestos.Remove(tratamientoPropuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details","PlanTratamientos", new { id = tratamientoPropuesto.PlanId });
        }

        private bool TratamientoPropuestoExists(int id)
        {
            return _context.tratamientosPropuestos.Any(e => e.Id == id);
        }
    }
}
