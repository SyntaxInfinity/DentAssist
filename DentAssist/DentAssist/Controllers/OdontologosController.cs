using DentAssist.Models.Data;
using DentAssist.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentAssist.Controllers
{
    public class OdontologosController : Controller
    {
        private readonly ClinicaDbContext _context;

        public OdontologosController(ClinicaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? odontologoId)
        {

            Console.WriteLine(odontologoId);
            var turnos = _context.turnos
                .Include(t => t.Paciente)
                .Where(t => t.OdontologoId == odontologoId)
                .OrderBy(t => t.Fecha)
                .ToList();
            ViewBag.OdontologoId = odontologoId;
            return View(turnos);
        }

        // GET: Odontologos
        public async Task<IActionResult> ListaOdontologos()
        {
            return View(await _context.odontologos.ToListAsync());
        }

        // GET: Odontologos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.odontologos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odontologo == null)
            {
                return NotFound();
            }

            return View(odontologo);
        }

        // GET: Odontologos/Create
        public IActionResult Create()
        {
            ViewBag.EspecialidadList = new SelectList(_context.especialidades, "Id", "NombreEspecialidad");
            return View(new Odontologo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Odontologo odontologo)
        {
            if (ModelState.IsValid)
            {
                _context.odontologos.Add(odontologo);
                _context.SaveChanges();
                return RedirectToAction("ListaOdontologos", "Odontologos");
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
            }

            ViewBag.EspecialidadList = new SelectList(_context.especialidades, "Id", "NombreEspecialidad", odontologo.EspecialidadId);
            return View(odontologo);
        }


        // GET: Odontologos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.odontologos.FindAsync(id);
            if (odontologo == null)
            {
                return NotFound();
            }
            return View(odontologo);
        }

        // POST: Odontologos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Matricula,Email,EspecialidadId,Password")] Odontologo odontologo)
        {
            if (id != odontologo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odontologo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdontologoExists(odontologo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ListaOdontologos", "Odontologos");
            }
            return View("ListaOdontologos", "Odontologos");
        }

        // GET: Odontologos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odontologo = await _context.odontologos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odontologo == null)
            {
                return NotFound();
            }

            return View(odontologo);
        }

        // POST: Odontologos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odontologo = await _context.odontologos.FindAsync(id);
            if (odontologo != null)
            {
                _context.odontologos.Remove(odontologo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ListaOdontologos","Odontologos");
        }

        private bool OdontologoExists(int id)
        {
            return _context.odontologos.Any(e => e.Id == id);
        }

        public IActionResult Perfil(int? id)
        {
            if (id == null)
            {
                if (TempData["OdontologoId"] != null)
                {
                    id = Convert.ToInt32(TempData["OdontologoId"]);
                    TempData.Keep("OdontologoId"); 
                }
                else
                {
                    return NotFound();
                }
            }

            var odontologo = _context.odontologos
                .Include(o => o.Especialidad)
                .FirstOrDefault(o => o.Id == id);

            if (odontologo == null)
            {
                return NotFound();
            }

            return View(odontologo);
        }
    }
}
