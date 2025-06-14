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
            return View(await _context.TratamientoPropuesto.ToListAsync());
        }

        // GET: TratamientoPropuestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoPropuesto = await _context.TratamientoPropuesto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tratamientoPropuesto == null)
            {
                return NotFound();
            }

            return View(tratamientoPropuesto);
        }

        // GET: TratamientoPropuestos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TratamientoPropuestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,planId,FechaEstimada,EstadoPeso,OrdenSecuencia,ObservacionesPaso")] TratamientoPropuesto tratamientoPropuesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamientoPropuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tratamientoPropuesto);
        }

        // GET: TratamientoPropuestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientoPropuesto = await _context.TratamientoPropuesto.FindAsync(id);
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

            var tratamientoPropuesto = await _context.TratamientoPropuesto
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
            var tratamientoPropuesto = await _context.TratamientoPropuesto.FindAsync(id);
            if (tratamientoPropuesto != null)
            {
                _context.TratamientoPropuesto.Remove(tratamientoPropuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoPropuestoExists(int id)
        {
            return _context.TratamientoPropuesto.Any(e => e.Id == id);
        }
    }
}
