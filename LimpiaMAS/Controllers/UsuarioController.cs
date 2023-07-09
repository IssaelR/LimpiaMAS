using LimpiaMAS.Models;
using LimpiaMAS.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LimpiaMAS.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexLimpiador() 
        {
            return View();
        }
        public IActionResult Disponibilidad()
        {
            return View();
        }
        public IActionResult IndexCliente()
        {
            return View();
        }
        [Route("Servicios")]
        public IActionResult Servicios()
        {
            return View();
        }
        [Route("SoporteTecnico")]
        public IActionResult SoporteTecnico()
        {
            return View();
        }
        [Route("Modificar")]
        public IActionResult Modificar()
        {
            return View();
        }
        [Route("Disponibilidad")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "Limpia");
        }
    }
}
