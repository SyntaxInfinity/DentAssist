using Microsoft.AspNetCore.Mvc;

using DentAssist.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DentAssist.Models.Data;

namespace DentAssist.Controllers
{
    public class InicioSesion : Controller
    {
        private readonly ClinicaDbContext _context;

        public InicioSesion(ClinicaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var usuario = _context.usuarios
                .FirstOrDefault(u => u.Correo == email && u.Password == password);

            if (usuario != null)
            {
                TempData["Usuario"] = usuario.Correo;
                TempData["Rol"] = usuario.Rol;

                switch (usuario.Rol.ToLower())
                {
                    case "administrador":
                        return RedirectToAction("Administrador", "Usuarios");
                    case "recepcionista":
                        return RedirectToAction("Recepcionista", "Usuarios");
                    default:
                        break;
                }
            }

            var odontologo = _context.odontologos
                .FirstOrDefault(o => o.Email == email && o.Password == password);

            if (odontologo != null)
            {
                TempData["Usuario"] = odontologo.Email;
                TempData["Rol"] = "Odontologo";
                TempData["OdontologoId"] = odontologo.Id;
                Console.WriteLine(odontologo.Id);
                return RedirectToAction("Index", "Odontologos", new { odontologoId = odontologo.Id });
            }

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }


    }
}

